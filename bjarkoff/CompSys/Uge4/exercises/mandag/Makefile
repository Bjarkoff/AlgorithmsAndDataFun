CC = gcc
CFLAGS = -O3 -Wall

all: cols rows

cols: sum-array-cols.c
	$(CC) $(CFLAGS) -o cols sum-array-cols.c

rows: sum-array-rows.c
	$(CC) $(CFLAGS) -o rows sum-array-rows.c

3darray: 3darray.c
	$(CC) $(CFLAGS) -o 3darray 3darray.c

clean:
	rm -f rows cols