#include <stdio.h>
#include <sys/types.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <fcntl.h>
#include <sys/wait.h>

int main(int argc, char* argv[]) {
  if (argc != 1) {
    printf("No arguments dude!");
    return EXIT_FAILURE;
  }
  else {
    pid_t pid;
    int status;
    printf("This is the start of the program, (pid:%d)\n", (int) getpid());
    printf("Also, I need to use argv, so here is argv[0]: %s\n", argv[0]);
    // int rc = fork();
    if ((pid = fork()) < 0) {
      // fork() malfunctioned
      fprintf(stderr, "the fork()-call malfunctioned!\n");
      return EXIT_FAILURE;
    }
    else if (pid == 0) {
      // child process
      
      printf("hello from child\n");
    }
    else {
      // parent process
      int rc_wait = waitpid(pid, &status, WUNTRACED); // pass the scheduled process to child-branch
      // note that here, we pass the pid-value from pid = fork(), thus it refers to the child process!
      if (rc_wait == -1) {
        fprintf(stderr, "malfunction calling waitpid!");
        return EXIT_FAILURE;
      }
      else {
      printf("goodbye from parent, wait-value is %d, status is %d\n", rc_wait, status);
      }
    }
    return EXIT_SUCCESS;
  }
}
