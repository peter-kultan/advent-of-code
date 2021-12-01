from typing import List, Optional


def determine_depth(path: str) -> int:
    result = 0
    with open(path) as my_file:
        previous_depth = int(my_file.readline().rstrip())
        for line in my_file:
            if previous_depth < int(line.rstrip()):
                result += 1
            previous_depth = int(line.rstrip())
    return result


def determine_depth_sum(path: str) -> int:
    result = 0
    numbers: List[int] = []
    previous_depth: Optional[int] = None
    with open(path) as my_file:
        for line in my_file:
            numbers.append(int(line.rstrip()))
            if len(numbers) == 3:
                if previous_depth is not None and\
                        sum(numbers) > previous_depth:
                    result += 1
                previous_depth = sum(numbers)
                numbers = numbers[1:]
    return result


if __name__ == "__main__":
    print(determine_depth("input.txt"))
    print(determine_depth_sum("input_measurement_windows.txt"))
