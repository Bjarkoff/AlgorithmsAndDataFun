#include <stdio.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>

int main() {
    pid_t pid = fork();

    if (pid == 0) {
        // This is the child process
        printf("Child process is running\n");
    } else if (pid > 0) {
        // This is the parent process
        int status; //Wait takes an int pointer
        // blocks the calling process until one of its child processes exits or a signal is received.
        pid_t wait_ret;
        wait_ret = wait(&status); // Stores status info in status, not needed for now
        printf("wait_ret: %d\n", wait_ret);
        printf("Child process has finished\n");
    } else {
        fprintf(stderr, "Fork failed.\n");
        return 1;
    }

    return 0;
}
