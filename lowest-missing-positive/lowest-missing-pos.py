import sys

input = [-1,0,-3,1,1,4,2]
input.sort()
length = len(input)

if (input[length - 1] <= 0):
    print("no pos")
    sys.exit(0)

for i in range(length - 1):
    x = input[i]
    y = input[i+1]

    if ((y - x > 1) and not(y + x <= y)):
        print(input[i]+1)
        sys.exit(0)

print(input[length - 1] + 1)
