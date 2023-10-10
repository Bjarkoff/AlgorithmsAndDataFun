#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>

int main(int argc, char **argv) {
    argc=argc;
    argv=argv;

    int x = 100;
    if (fork() == 0) {
        //x = x + 1;
        //Variables copied into childs own memory space to note address might be the same!
        printf("Child set x to %d\n", x);
        //printf("Child, x address is %p\n", (void*)&x);
    } else {
        x = x + 1;
        printf("Parent set x to %d\n", x);
        //printf("Parent, x address is %p\n", (void*)&x);
    }
    printf("I am process %d, and my x is: %d\n", getpid(), x);
    return 0;

}