#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/wait.h>

// # define NULL = __DARWIN_NULL;
// # define stderr __stderrp;

int main(void) {
  printf("hello world (pid:%d)\n", (int) getpid());
  int x = 100;
  int rc = fork();
  if (rc < 0) {
    // fork failed
    fprintf(stderr, "fork failed\n");
  exit(1);
  } else if (rc == 0) {
    // child (new process)
    printf("hello, I am child (pid:%d), and value x is: %d\n", (int) getpid(), x);
    x++;
  } else {
    // parent goes down this path (main)
    // int rc_wait = wait(NULL);
    printf("hello, I am parent of %d (pid:%d), and value x is: %d\n",
     rc, (int) getpid(), x);
    x++;
  }
  return 0;
  // suprisingly, changing value x in any of the sub-branches of main do NOT change
// the value of x when calling another branch! This must be because each branch has its
// own virtual CPU and memory of x.
}


