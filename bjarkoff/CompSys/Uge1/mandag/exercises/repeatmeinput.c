/* This may look like nonsense , but really is -*- mode : C -*- */
# include <stdlib.h>
# include <stdio.h>
// # include <conio.h>

/* The main thing that this program does . */
int main(int argn, char* argc[]) {
  // Doing some work
  if (argn != 2) {
    printf("wrong number of arguments!");
      return EXIT_FAILURE;
    }
  else {
    char* inputString = argc[1];
    printf("%s\n%s", inputString, inputString);
    return EXIT_SUCCESS;
  }
}
