#/bin/bash

set -e


gcc -o insertion_sort_test insertion_sort_test.c -ggdb
./insertion_sort_test

gcc -o merge_sort_test merge_sort_test.c -ggdb
./merge_sort_test
