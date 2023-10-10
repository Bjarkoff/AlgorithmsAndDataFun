j main

.data
.data1:
    .ascii "123424"
.needle:
    .ascii "23"


.text
.include "utils.asm"
.include "memfind.asm"
main:

    # Load adresses into a0, a1
    # Load lengths into a2, a3
    la a0, .data1
    la a1, .needle
    li a2, 6
    li a3, 2
    call memfind

    call printchar

    call exit0


