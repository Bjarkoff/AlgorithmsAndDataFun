#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <fcntl.h>
#include <sys/wait.h>

int main(void) {
  printf("dette er starten af programmet, moder-pid er: (pid:%d)\n",(int) getpid());
  // redirect standard output to a file
  close(STDOUT_FILENO);
  int op = open("osteptest.txt", O_CREAT|O_WRONLY|O_TRUNC, S_IRWXU);
  int rc = fork();
  if (rc < 0) {
    // fork failed
  }
  else if (rc == 0) {
    // child process
    for (size_t i = 0; i<5; ++i) {
      printf("this child text should go in .txt file, op is: %d\n", op);
      // sleep(1);
    }
    
  }
  else {
    // mother process
    for (size_t i = 0; i<5; ++i) {
      printf("this parent text should go in .txt file, op is: %d\n", op);
      // sleep(1);
    }
  }
  close(op);
  // by looking at content of osteptest.txt, this reveals that while one process is sleeping,
  // the other process may take over, and vice versa, so that no specific ordering can be had.
  // but we need the "sleep(1)" to see this.
}
