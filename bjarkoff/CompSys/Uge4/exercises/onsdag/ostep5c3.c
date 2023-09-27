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
      printf("hello from child\n");
    }
    else {
      // parent process
      sleep(1); // try to pass the active process to child-branch
      printf("goodbye from parent\n");
    }
    return EXIT_SUCCESS;
  }
}
