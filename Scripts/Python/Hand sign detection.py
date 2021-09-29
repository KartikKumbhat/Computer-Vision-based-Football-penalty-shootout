import copy
import cv2
import numpy as np
from keras.models import load_model
from phue import Bridge
from soco import SoCo
import pygame
import time
import pyautogui as key
import socket
import math

UDP_IP = "127.0.0.1"
UDP_PORT = 5065

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)


prediction = ''
action = ''
score = 0
img_counter = 500

save_images, selected_gesture = False, 'peace'
start_detection = True
start_gesture = True

gesture_names = {0: 'Fist', 
                 1: 'L',
                 2: 'Okay',
                 3: 'Palm',
                 4: 'Peace'}

#Enter the path of VGG_cross_validated.h5
model = load_model('Model/VGG_cross_validated.h5')


def predict_rgb_image(img):
    result = gesture_names[model.predict_classes(img)[0]]
    print(result)
    return (result)


def predict_rgb_image_vgg(image):
    image = np.array(image, dtype='float32')
    image /= 255
    pred_array = model.predict(image)
    print(f'pred_array: {pred_array}')
    result = gesture_names[np.argmax(pred_array)]
    print(f'Result: {result}')
    print(max(pred_array[0]))
    score = float("%0.2f" % (max(pred_array[0]) * 100))
    print(result)
    return result, score


# parameters
cap_region_x_begin = 0.5  # start point/total width
cap_region_y_end = 0.8  # start point/total width
threshold = 60  # binary threshold
blurValue = 41  # GaussianBlur parameter
bgSubThreshold = 50
learningRate = 0

# variableslt
isBgCaptured = 0  
triggerSwitch = False  


def remove_background(frame):
    fgmask = bgModel.apply(frame, learningRate=learningRate)
    kernel = np.ones((3, 3), np.uint8)
    fgmask = cv2.erode(fgmask, kernel, iterations=1)
    res = cv2.bitwise_and(frame, frame, mask=fgmask)
    return res


# Camera
camera = cv2.VideoCapture(0)
camera.set(10, 200)

while camera.isOpened():
    ret, frame = camera.read()
    frame = cv2.bilateralFilter(frame, 5, 50, 100)  # smoothing filter
    frame = cv2.flip(frame, 1)  # flip the frame horizontally
    cv2.rectangle(frame, (int(cap_region_x_begin * frame.shape[1]), 0),
                  (frame.shape[1], int(cap_region_y_end * frame.shape[0])), (255, 0, 0), 2)

    cv2.imshow('original', frame)

    # Run once background is captured
    if isBgCaptured == 1:
        img = remove_background(frame)
        img = img[0:int(cap_region_y_end * frame.shape[0]),
              int(cap_region_x_begin * frame.shape[1]):frame.shape[1]]  # clip the ROI
        gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        blur = cv2.GaussianBlur(gray, (blurValue, blurValue), 0)
        ret, thresh = cv2.threshold(blur, threshold, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
       
        cv2.putText(thresh, f"Prediction: {prediction} ({score}%)", (50, 30), cv2.FONT_HERSHEY_SIMPLEX, 1,
                    (255, 255, 255))
        cv2.putText(thresh, f"Action: {action}", (50, 80), cv2.FONT_HERSHEY_SIMPLEX, 1,
                    (255, 255, 255))  # Draw the text
        #cv2.imshow('ori', thresh)

        thresh1 = copy.deepcopy(thresh)
        contours, hierarchy = cv2.findContours(thresh1, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
        length = len(contours)
        maxArea = -1
        if length > 0:
            for i in range(length): 
                temp = contours[i]
                area = cv2.contourArea(temp)
                if area > maxArea:
                    maxArea = area
                    ci = i

            res = contours[ci]
            hull = cv2.convexHull(res)
            drawing = np.zeros(img.shape, np.uint8)
            cv2.drawContours(drawing, [res], 0, (0, 255, 0), 2)
            cv2.drawContours(drawing, [hull], 0, (0, 0, 255), 3)

        #cv2.imshow('output', drawing)

    
    k = cv2.waitKey(10)
    
    

    if k == 27:  # press ESC to exit all windows at any time
        break
    elif k == ord('b'):  # press 'b' to capture the background
        bgModel = cv2.createBackgroundSubtractorMOG2(0, bgSubThreshold)
        time.sleep(2)
        isBgCaptured = 1
        print('Background captured')


        
 
    elif k == ord('r'):  # press 'r' to reset the background
        time.sleep(1)
        bgModel = None
        triggerSwitch = False
        isBgCaptured = 0
        print('Reset background')
    
    elif k == 32:
        cv2.waitKey(2000)
        cv2.imshow('original', frame)
        target = np.stack((thresh,) * 3, axis=-1)
        target = cv2.resize(target, (224, 224))
        target = target.reshape(1, 224, 224, 3)
        prediction, score = predict_rgb_image_vgg(target)

        if start_detection:
            
            if prediction == 'Fist':
                try:
                    action = 'Top left corner'
                    MESSAGE = "TLC"
                    sock.sendto(bytes(MESSAGE, "utf-8"), (UDP_IP, UDP_PORT))
                    print(action)
                            
                except ConnectionError:
                    print("not worked")
                    pass

            elif prediction == 'L':
                try:
                    action = 'top right'
                    print(action)
                    MESSAGE = "TRC"
                    sock.sendto(bytes(MESSAGE, "utf-8"), (UDP_IP, UDP_PORT))

                except ConnectionError:
                    print("not worked")
                    pass

            elif prediction == 'Okay':
                try:
                    action = 'left bottom'
                    print(action)
                    MESSAGE = "BLC"
                    sock.sendto(bytes(MESSAGE, "utf-8"), (UDP_IP, UDP_PORT))
                            
                except ConnectionError:
                    print("not worked")
                    pass

            elif prediction == 'Peace':
                try:
                    print("Bottom right corner")
                    MESSAGE = "BRC"
                    sock.sendto(bytes(MESSAGE, "utf-8"), (UDP_IP, UDP_PORT))
                            
                except ConnectionError:
                    print("not worked")
                    pass

            else:
                pass
        
    elif k == ord('t'):
        cv2.destroyAllWindows()
        cap.release()