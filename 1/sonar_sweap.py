def determine_depth(path: str) -> int:
    result = 0
    with open(path) as my_file:
        previous_depth = int(my_file.readline().rstrip())
        for line in my_file:
            if previous_depth < int(line.rstrip()):
                result += 1
            previous_depth = int(line.rstrip())
    return result


if __name__ == "__main__":
    print(determine_depth("input.txt"))
