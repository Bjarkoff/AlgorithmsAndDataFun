// We convert the RISC-V code into C-language instructions.

/*
slli  x30, x5,  2   # x30 = f * 4
add   x30, x10, x30 # x30 = &A[f]
slli  x31, x6,  2   # x31 = g * 4
add   x31, x11, x31 # x31 = &B[g]
lw    x5, 0(x30)    # f = A[f]

addi x12, x30, 4
lw   x30, 0(x12)
add  x30, x30, x5
sw   x30, 0(x31)
*/

// we assume that f, g, h, i, j are assigned registers x5, x6, x7, x28, x29. And that
// arrays A and B have base-address x10 and x11, respectively.

f = A[f];
x = A[f+1];
B[g] = A[f]+A[f+1]