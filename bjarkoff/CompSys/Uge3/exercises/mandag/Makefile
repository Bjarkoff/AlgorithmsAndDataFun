CC=gcc
CFLAGS=-std=c11 -Wall -Werror -Wextra -pedantic -g

.PHONY: clean all

cmd: updatedfile(s).c
	$(CC) $(CFLAGS) -o $@ -c $<

hexabyte: hexabyte.c
	$(CC) -o hexabyte hexabyte.c $(CFLAGS)

int2ascii: int2ascii.c
	$(CC) -o int2ascii int2ascii.c $(CFLAGS)

ascii2int: ascii2int.c
	$(CC) -o ascii2int ascii2int.c $(CFLAGS)

roll: roll.c
	$(CC) -o roll roll.c $(CFLAGS)

hello: hello.c
	$(CC) -o hello hello.c $(CFLAGS)	