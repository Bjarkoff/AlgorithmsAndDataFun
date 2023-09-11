/*
STB style header-only insertion sort implementation.
Define INSERTION_SORT_IMPL before including this file in one source file to include the implementation.

Is made generic by using the preprocessor.
Define INSERTION_SORT_TYPE to decide what type to sort.
Define INSERTION_SORT_CMP(x, y) to decide how to compare two elements.
INSERTION_SORT_CMP(x, y) should return true if x should go before y.

The insertion_sort function will be called insertion_sort_INSERTION_SORT_TYPE, e.g. insertion_sort_int.


Example usage:
#define INSERTION_SORT_IMPL
#define INSERTION_SORT_TYPE int
#define INSERTION_SORT_CMP(x, y) (x < y)
#include "insertion_sort.h"


insertion_sort_int(my_array, 10);
*/
#ifdef INSERTION_SORT_TYPE

#define concat(x, y) x ## y
#define FNAME(x) concat(insertion_sort_, x)
void FNAME(INSERTION_SORT_TYPE)(INSERTION_SORT_TYPE *array, int size);

#ifdef INSERTION_SORT_IMPL

void FNAME(INSERTION_SORT_TYPE)(INSERTION_SORT_TYPE *array, int size) {
    for (int i = 1; i < size; i++) {
        INSERTION_SORT_TYPE key = array[i];
        int j = i - 1;
        while (j >= 0 && INSERTION_SORT_CMP(key, array[j])) {
            array[j + 1] = array[j];
            j--;
        }
        array[j + 1] = key;
    }
}
#endif // INSERTION_SORT_IMPL
#undef concat 
#undef FNAME
#undef INSERTION_SORT_TYPE
#undef INSERTION_SORT_CMP
#endif // INSERTION_SORT_TYPE

