# We are to write RISC-V code for B[8] = A[i-j],
# assuming that f, g, h, i, j are assigned to registers x5, x6, x7, x28 and x29 respectively.
# Also we assume the base address of array A and B are in registers x10 and x11, respectively.

sub x5, x28, x29 # we calculate i-j
add x5, x5, x10 # we add the base of A, so that x5 is now address of A[i-j]
lb x5, 0(x5) # we load the value of A[i-j], f = Mem[f] = A[i-j]
sb x5, 8(x11) # we write to B[8], B[8] = f = A[i-j]

# This assumes byte-arrays. If we assume each word is 4 bytes, we would augment by
# 1) multypliing i-j by 4, by sliding each bit 2 to the left, ssli x5, x5, 2
# 2) multiplying the address of B by 32 (4*8) instead of 8.