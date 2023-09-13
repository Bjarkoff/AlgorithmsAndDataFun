#include <stdio.h>
#include <stdlib.h>
#include <assert.h>

int read_int(FILE* f, int *out) {
  
}

int main(int argc, char* argv[]) {
  if (argc != 2) {
    printf("Bad value of input\n");
    return EXIT_FAILURE;
  }
  FILE *f = fopen(argv[1], "r");
  assert(f != NULL);
  char c;
  while (fread(&c, sizeof(char), 1, f) == 1) {
  printf("0x%.2x ", (int)c);
  printf("\n");
  }
  return EXIT_SUCCESS;
}
