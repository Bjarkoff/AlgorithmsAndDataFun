#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <fcntl.h>
#include <string.h>

int main() {
    int fd;
    char *filename = "test.txt";

    //Sets permits etc, lookup man page for options consts, but dont worry about them for now
    fd = open(filename, O_WRONLY | O_TRUNC, S_IRUSR | S_IWUSR | S_IRGRP | S_IROTH);

    if (fd < 0) {
        perror("open");
        exit(1);
    }

    pid_t pid = fork();

    if (pid == 0) {
        // This is the child process
        char *child_message = "Message from child process.\n";
        for (size_t i = 0; i < 100; i++)
        {
            write(fd, child_message, strlen(child_message));
            printf("Child wrote to file.\n");
        }
        

    } else if (pid > 0) {
        // This is the parent process
        char *parent_message = "Message from parent process.\n";
        write(fd, parent_message, strlen(parent_message));
        for (size_t i = 0; i < 100; i++)
        {
            write(fd, parent_message, strlen(parent_message));
            printf("Child wrote to file.\n");
        }
        printf("Parent wrote to file.\n");
    } else {
        perror("fork");
        exit(1);
    }

    close(fd);

    return 0;
}
