#include <stdio.h>
#include <stdlib.h>
#include <assert.h>

int read_ascii_int(FILE* f, int *out) {
  char c;
  int x = 0;
  while (fread(&c, sizeof(char), 1, f)) {
    if (c == '\n') {
      *out = x;
      return EXIT_SUCCESS;
    }
    int d = (int)(c - '0');
    x = x * 10 + d;
  };
  return EXIT_FAILURE;
}

int main(int argc, char* argv[]) {
  if (argc != 3) {
    printf("You need exactly 2 arguments to run this file");
    return EXIT_FAILURE;
  }
  FILE *f = fopen(argv[1], "r");
  FILE *g = fopen(argv[2], "w");
  int x;
  assert(f != NULL);
  while (read_ascii_int(f, &x) == 0) {
  printf("%d\n", (int)x);
  fwrite(&x, sizeof(int), 1, g);
  }
  return EXIT_SUCCESS;
}
