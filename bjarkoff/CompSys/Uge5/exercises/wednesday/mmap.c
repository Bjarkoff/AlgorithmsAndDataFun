#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <fcntl.h>
#include <sys/wait.h>
#include <sys/mman.h>
#include <assert.h>

size_t file_size(FILE* f) {
  assert(fseek(f, 0, SEEK_END) == 0);
  int end = ftell(f);
  assert(fseek(f, 0, SEEK_SET) == 0);
  return end;
}

int main(int argc, char* argv[]) {
  if (argc != 2) {
    fprintf(stderr,"program needs a path to file as argument!");
    return EXIT_FAILURE;
  }
  const char *filepath = argv[1];
  FILE* f = fopen(filepath,"r+");
  size_t size = file_size(f);
  unsigned char *data = mmap(NULL,
                             size,
                             PROT_WRITE | PROT_READ,
                             MAP_SHARED,
                             fileno(f), 0);
  assert(data != MAP_FAILED);
  fclose(f);
  for (size_t i = 0; i < size; i++) {
    printf("value of entry %zu: %c\n",i,data[i]);
    if (data[i] == 'C') {
      data[i] = 'D';
    }
    else if (data[i] == 'o') {
      data[i] = 'u';
    }
    else if (data[i] == 'p') {
      data[i] = 'b';
    }
  }
  
  if (msync(data, size, MS_SYNC) == -1) {
    perror("msync");
    return EXIT_FAILURE;
  }
  if (munmap(data, size) == -1) {
    perror("munmap");
    return EXIT_FAILURE;
  }
  
  return EXIT_SUCCESS;

}
