set -xe
gcc -o main stack_use.c -std=c11 -ggdb -Wno-incompatible-pointer-types
./main
