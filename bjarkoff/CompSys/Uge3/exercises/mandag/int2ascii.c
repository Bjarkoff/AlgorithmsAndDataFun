#include <stdio.h>
#include <stdlib.h>
#include <assert.h>

int read_int(FILE* f, int *out) {
  return (fread(out, sizeof(int), 1, f) + 1) % 2; // returns 0 when fread == 1, and 1 when fread == 0.
}
// Important note to self: Originallt above I wrote "fread(&out,...)", but since the input
// is already int *out, that means (I think) that it is the pointer to out that is given,
// not the int out itself. Therefore no need to go to pointer via &out, since out is already
// the pointer.

int main(int argc, char* argv[]) {
  if (argc != 2) {
    printf("Bad value of input\n");
    return EXIT_FAILURE;
  }
  FILE *f = fopen(argv[1], "r");
  int x;
  assert(f != NULL);
  while (read_int(f, &x) == 0) {
  printf("%d", (int)x);
  printf("\n");
  }
  return EXIT_SUCCESS;
}
