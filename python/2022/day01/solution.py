#!/bin/python3
print("My first python program ever ")
f = open("input.txt","r")
lines = f.readlines()

elves = []
sum= 0

for line in lines:
        if line == "\n":
                elves.append(sum)
                sum = 0
        else:
                sum += int(line)

elves.sort(reverse = True)

print("part 1",elves[0])
print("part 2",elves[0]+elves[1]+elves[2])

