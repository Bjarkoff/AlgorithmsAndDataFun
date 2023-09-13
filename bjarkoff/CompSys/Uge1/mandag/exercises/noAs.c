/* This may look like nonsense , but really is -*- mode : C -*- */
# include <stdlib.h>
# include <stdio.h>

/* The main thing that this program does . */
int main(int argn, char* argc[]) {
  // Input argument validation
  if (argn != 2) {
    printf("wrong number of arguments!");
      return EXIT_FAILURE;
    }
  else if (argc[1][0] == 'A') {
    printf("No beginning A's are allowed");
    return EXIT_FAILURE;
  }  
  // Main work is done
  else {
    char* inputString = argc[1];
    printf("%s",inputString);
    return EXIT_SUCCESS;
  }
}
