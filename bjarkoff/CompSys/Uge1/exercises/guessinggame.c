/* This is the start of the program*/
# include <stdio.h>
# include <stdlib.h>
#include <time.h>

int main(int argc, char* argv[]) {
  srand(time(NULL));
  int ran = rand() % 1000; 
  int input; 
  if (argc == 2) {
    int res = sscanf(argv[1],"%d", &input);
    if (res == 0) {
      printf("Bad value of input\n");
    } else {
      if (input > ran) {
        printf("your number %d was higher than random number %d!",
        input,ran);
        return EXIT_FAILURE;
      } else if (input < ran) {
        printf("your number %d was lower than random number %d!",
        input,ran);
      } else {
        printf("your number %d was equal to random number %d!",
        input,ran);
      }
    }
  }
  return EXIT_SUCCESS;
}
