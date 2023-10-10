#include <stdio.h>
#include <unistd.h>
#include <signal.h>

volatile sig_atomic_t flag = 0;

void childSignalHandler(int signo) {
    signo = signo; //To avoid
    flag = 1;
}

int main() {
    //Sets up sighandler for SIGUSR1 in parent
    // When the child process sends this signal, the childSignalHandler function will be called.
    signal(SIGCHLD, childSignalHandler);

    pid_t pid = fork();

    if (pid == 0) {
        // This is the child process
        printf("hello\n");
        kill(getppid(), SIGCHLD); // Send signal to parent
    } else if (pid > 0) {
        // This is the parent process
        while (!flag); // Wait for signal from child, so waits until becomes 1
        printf("goodbye\n");
    } else {
        fprintf(stderr, "Fork failed.\n");
        return 1;
    }

    return 0;
}
