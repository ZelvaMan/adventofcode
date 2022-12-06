print("DAY 2022 06")
#MUST BE RUN FROM python folder
f = open("../inputs/2022/06/i.txt")

line = f.readline()


def findFirstSequence(lenght):
    for index in range(len(line)-lenght):
        substing = line[index:index+lenght]
        if (len(set(substing)) == lenght):
            return index+lenght


findFirstSequence(4)
findFirstSequence(14)
