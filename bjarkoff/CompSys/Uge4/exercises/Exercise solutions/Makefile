CC=gcc
CFLAGS=-Wall -Wextra -pedantic
PROGRAMS=q1 q2 q3 q5 q6

all: $(PROGRAMS)

%: %.c
	$(CC) -o $@ $^ $(CFLAGS)


clean:
	rm -rf q1 q2 q3 q5 q6