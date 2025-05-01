import random

arr = [random.randint(10, 99)]
for i in range(19):
    arr.append(random.randint(arr[-1], 99))
    
print("A tömb elemei:", arr)    