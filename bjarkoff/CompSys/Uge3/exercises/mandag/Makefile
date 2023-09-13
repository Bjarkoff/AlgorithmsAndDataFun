CC=gcc
CFLAGS=-std=c11 -Wall -Werror -Wextra -pedantic -g

.PHONY: clean all

cmd: updatedfile(s).c
	$(CC) $(CFLAGS) -o $@ -c $<

hexabyte: hexabyte.c
	$(CC) -o hexabyte hexabyte.c $(CFLAGS)

repeatmeinput: repeatmeinput.c
	$(CC) -o repeatmeinput repeatmeinput.c $(CFLAGS)

guessinggame: guessinggame.c
	$(CC) -o guessinggame guessinggame.c $(CFLAGS)

roll: roll.c
	$(CC) -o roll roll.c $(CFLAGS)