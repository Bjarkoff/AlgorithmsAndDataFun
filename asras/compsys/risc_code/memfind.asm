.globl memfind
.include "memeq.asm"


memfind:
    # Reserve words for: ra, current address of haystack, address of needle, length of haystack, length of needle, t0
    addi sp, sp, -24
    sw ra, 0(sp)
    sw a0, 4(sp)
    sw a1, 8(sp)
    sw a2, 12(sp)
    sw a3, 16(sp)
    # Store remaining length of haystack
    mv t1, a2


memfind.loop:
    # If there is less than the needle length left in the haystack we didn't find the needle
    lw a3, 16(sp) # Load needle length
    blt t1, a3, memfind.not_found 

    # Try to find the needle at the current position
    # Call memeq by loading pointer to haystack and needle into a0, a1
    # and length of needle into a2
    lw a0, 4(sp)  # Haystack address
    lw a1, 8(sp)  # Needle address
    lw a2, 16(sp) # Needle length
    sw t1, 20(sp) # Store current remaining lengthmemfind. (Is t2 caller or callee saves?)
    call memeq
    lw t1, 20(sp) # Restore current remaining length


    bne zero, a0, memfind.found

    # Decrement remaining length
    addi t1, t2, -1
    # Increment pointer
    lw a0, 4(sp)
    addi a0, a0, 1
    sw a0, 4(sp)
    j memfind.loop


memfind.found: 
    li a0, 1
    j memfind.ret


memfind.not_found:
    li a0, 0
    j memfind.ret


memfind.ret:
    lw ra, 0(sp)
    addi sp, sp, 24
    ret
