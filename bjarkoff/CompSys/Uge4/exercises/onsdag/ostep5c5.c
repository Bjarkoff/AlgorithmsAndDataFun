#include <stdio.h>
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
    printf("This is the start of the program, (pid:%d)\n", (int) getpid());
    printf("Also, I need to use argv, so here is argv[0]: %s\n", argv[0]);
    int rc = fork();
    if (rc < 0) {
      // fork() malfunctioned
      fprintf(stderr, "the fork()-call malfunctioned!\n");
      return EXIT_FAILURE;
    }
    else if (rc == 0) {
      // child process
      int rc_wait = wait(NULL); // confuse thngs with the parent branch
      printf("hello from child, wait-value is %d\n", rc_wait);
    }
    else {
      // parent process
      int rc_wait = wait(NULL); // pass the scheduled process to child-branch
      printf("goodbye from parent, wait-value is %d\n", rc_wait);
    }
    return EXIT_SUCCESS;
    // Interestingly, the wait-value from the child-process is -1,
    // suggesting that you cannot wait twice, and that the 
    // second such call will result in an error. Makes sense, though.
  }
}
