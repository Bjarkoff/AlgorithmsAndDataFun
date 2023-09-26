#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#define BAD_INPUT 0

/* You might want to use different values for L,M,N when performing benchmarks. */
#define L 500
#define M 500
#define N 500

long sum_array_3d(long a[L][M][N])
{
  long sum = 0;
  int i, j, k;
  for (i = 0; i < L; i++) {
    for (j = 0; j < M; j++) {
      for (k = 0; k < N; k++) {
        sum += a[k][j][i]; // before it was k,i,j
      }
    }
  }
  return sum;
}

int main (void) {
  
  // Allocate the memory on the heap
  long (*input)[M][N] = malloc(sizeof(long) * L * M * N);

  // Check if the memory allocation was successful
  if (input == NULL) {
    fprintf(stderr, "Memory allocation failed!\n");
    return EXIT_FAILURE;
  }

  // Initialize the array
  for (int i = 0; i < L; i++) {
    for (int j = 0; j < M; j++) {
      for (int k = 0; k < N; k++) {
        (input)[k][j][i] = 1;
      }
    }
  }

  struct timespec start, end;
  clock_gettime(CLOCK_MONOTONIC, &start);
  // Use volatile to prevent the compiler from optimizing this away!
  volatile long l = sum_array_3d(input);

  clock_gettime(CLOCK_MONOTONIC, &end);
  long long elapsed = (end.tv_sec - start.tv_sec) * 1e9 + (end.tv_nsec - start.tv_nsec);
  printf("Sum is: %ld\n", l);
  printf("Time elapsed: %lld ns\n", elapsed);
  // Free the allocated memory
  free(input);

  return EXIT_SUCCESS;
}

