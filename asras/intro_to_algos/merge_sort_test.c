// Include sorting of ints
#define MERGE_SORT_IMPL
#define MERGE_SORT_TYPE int
#define MERGE_SORT_CMP(x, y) (x < y)
#include "merge_sort.h"

// // Include sorting of floats
#define MERGE_SORT_IMPL
#define MERGE_SORT_TYPE float
#define MERGE_SORT_CMP(x, y) (x < y)
#include "merge_sort.h"


// // Include sorting of custom type
typedef struct Point {
    float x;
    float y;
} Point;
#define MERGE_SORT_IMPL
#define MERGE_SORT_TYPE Point
// Lexicographic sorting
#define MERGE_SORT_CMP(pt1, pt2) ((pt1.x < pt2.x) || (pt1.x == pt2.x && pt1.y < pt2.y))
#include "merge_sort.h"


#include <assert.h> // For testing
#include <stdlib.h> // For rand()
#include <stdio.h> // For printf                    

void test_int_sort(void) {
    int arr[] = { 5, 4, 3, 2, 1 };
    merge_sort_int(arr, 5);
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
    merge_sort_int(big_test, size);
    for (int i = 0; i < size - 1; i++) {
        assert(big_test[i] <= big_test[i + 1]);
    }
    printf("Int test passed\n");
}

void test_float_sort(void) {
    int size = 1000;
    float test[size];
    float max_value = 10000.0f;
    float min_value = -10000.0f;
    for (int i = 0; i < size; i++) {
        test[i] = ((float)rand() / RAND_MAX) * (max_value - min_value) + min_value;
    }
    merge_sort_float(test, size);
    for (int i = 0; i < size - 1; i++) {
        assert(test[i] < test[i + 1]);
    }
    printf("Float test passed\n");
}


#define PT_EQ(pt, v1, v2) ((pt.x == v1) && (pt.y == v2))
void test_point_sort(void) {
    Point pt1 = { 1.0, 1.0};
    Point pt2 = { -1.0, 1.0};
    Point pt3 = { 1.0, -1.0};
    Point pt4 = { 2.0, -1.0};
    Point pts[4] = {pt1, pt2, pt3, pt4};
    merge_sort_Point(pts, 4);
    assert(PT_EQ(pts[0], -1.0, 1.0));
    assert(PT_EQ(pts[1], 1.0, -1.0));
    assert(PT_EQ(pts[2], 1.0, 1.0));
    assert(PT_EQ(pts[3], 2.0, -1.0));
    
    printf("Point test passed\n");
}

int main(void) {
    printf("##################################################\n");
    printf("                 MERGE SORT TESTS\n");
    printf("##################################################\n");
    test_int_sort();
    test_float_sort();
    test_point_sort();

    return 0;
}

