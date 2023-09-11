// Include sorting of ints
#define INSERTION_SORT_IMPL
#define INSERTION_SORT_TYPE int
#define INSERTION_SORT_CMP(x, y) (x < y)
#include "insertion_sort.h"

// // Include sorting of floats
#define INSERTION_SORT_IMPL
#define INSERTION_SORT_TYPE float
#define INSERTION_SORT_CMP(x, y) (x < y)
#include "insertion_sort.h"


// // Include sorting of custom type
typedef struct Point {
    float x;
    float y;
} Point;
#define INSERTION_SORT_IMPL
#define INSERTION_SORT_TYPE Point
// Lexicographic sorting
#define INSERTION_SORT_CMP(pt1, pt2) ((pt1.x < pt2.x) || (pt1.x == pt2.x && pt1.y < pt2.y))
#include "insertion_sort.h"


#include <assert.h> // For testing
#include <stdlib.h> // For rand()

void test_int_sort(void) {
    int arr[] = { 5, 4, 3, 2, 1 };
    insertion_sort_int(arr, 5);
    assert(arr[0] == 1);
    assert(arr[1] == 2);
    assert(arr[2] == 3);
    assert(arr[3] == 4);
    assert(arr[4] == 5);


    int size = 1000;
    int big_test[size];
    int max_value = 100000;
    for (int i = 0; i < size; i++) {
        big_test[i] = rand() % max_value;
    }
    insertion_sort_int(big_test, size);
    for (int i = 0; i < size - 1; i++) {
        assert(big_test[i] <= big_test[i + 1]);
    }
}

int main(void) {
    test_int_sort();

    return 0;
}

