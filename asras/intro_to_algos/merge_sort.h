/*
STB style header-only merge sort implementation.
Define MERGE_SORT_IMPL before including this file in one source file to include the implementation.

Is made generic by using the preprocessor.
Define MERGE_SORT_TYPE to decide what type to sort.
Define MERGE_SORT_CMP(x, y) to decide how to compare two elements.
MERGE_SORT_CMP(x, y) should return true if x should go before y.

The merge_sort function will be called merge_sort_MERGE_SORT_TYPE, e.g. merge_sort_int.


Example usage:
#define MERGE_SORT_IMPL
#define MERGE_SORT_TYPE int
#define MERGE_SORT_CMP(x, y) (x < y)
#include "merge_sort.h"


merge_sort_int(my_array, 10);
*/

#ifdef MERGE_SORT_TYPE

#define concat(x, y) x ## y
#define FNAME(x) concat(merge_sort_, x)
#define UTIL(x, y) concat(x, y)

void FNAME(MERGE_SORT_TYPE)(MERGE_SORT_TYPE *array, int size);

#ifdef MERGE_SORT_IMPL
#include <assert.h>

#define MERGE_NAME UTIL(merge_, MERGE_SORT_TYPE)
void MERGE_NAME(MERGE_SORT_TYPE *arr, int p, int q, int r) {
    int left_size = q - p;
    int right_size = r - q;


    MERGE_SORT_TYPE left[left_size];
    MERGE_SORT_TYPE right[right_size];

    for (int i = 0; i < left_size; i++) {
        left[i] = arr[p + i];
    }
    for (int i = 0; i < right_size; i++) {
        right[i] = arr[q + i];
    }

    int left_idx = 0;
    int right_idx = 0;
    int merge_idx = p;

    while (left_idx < left_size && right_idx < right_size) {
        MERGE_SORT_TYPE left_val = left[left_idx];
        MERGE_SORT_TYPE right_val = right[right_idx];

        if (MERGE_SORT_CMP(left_val, right_val)) {
            arr[merge_idx] = left_val;
            left_idx++;
        } else {
            arr[merge_idx] = right_val;
            right_idx++;
        }

        merge_idx++;
    }

    while (left_idx < left_size) {
        arr[merge_idx++] = left[left_idx++];
    }
    while (right_idx < right_size) {
        arr[merge_idx++] = right[right_idx++];
    }

}

#define IMPL_NAME UTIL(merge_sort_impl_, MERGE_SORT_TYPE)
void IMPL_NAME(MERGE_SORT_TYPE *array, int start_idx, int end_idx) {
    // Start included, end excluded
    if (start_idx >= end_idx - 1) {
        return;
    }

    int q = (start_idx + end_idx) / 2;
    IMPL_NAME(array, start_idx, q);
    IMPL_NAME(array, q, end_idx);
    MERGE_NAME(array, start_idx, q, end_idx);
}

void FNAME(MERGE_SORT_TYPE)(MERGE_SORT_TYPE *array, int size) {
    UTIL(merge_sort_impl_, MERGE_SORT_TYPE)(array, 0, size);
}

#endif // MERGE_SORT_IMPL
#undef concat 
#undef FNAME
#undef MERGE_SORT_TYPE
#undef MERGE_SORT_CMP
#undef MERGE_NAME
#undef IMPL_NAME
#endif // MERGE_SORT_TYPE
