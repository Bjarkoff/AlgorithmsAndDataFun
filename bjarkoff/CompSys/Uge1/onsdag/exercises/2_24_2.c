#include <stdlib.h>
#include <stdio.h>
int main(void) {
  int acc = 0;
  for (size_t i = 10; i; --i) {
    acc += 2;
  }

  // eller med et while loop:
  // size_t i = 10;
  // while (i) {
  //   --i;
  //   acc += 2;
  // }
}

/* Exercise 2.24.3: If x6 is initialized to N, then the 4 lines are iterated N times (4*N),
and then a final line 1 again to check, and then the Done: is not run? (because there is no 
instructions inside this line?). So 4N+1.
 */

// Exercise 2.24.4: We replace the beq with blt. Then the corresponding C-code looks like this:

int main2(void) {
  int acc = 0;

  size_t i = 10;
  while (i >= 0) {
    --i;
    acc += 2;
  }
}