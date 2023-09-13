# We need to write assembly RISC-V code for the following function:
# fib(n) if n < 2 then return 1 else return fib(n-1) + fib(n-2).
init:
        li x10, 6     # Load 5 into x10
        jal ra, fib  # Call fact procedure
        jal zero, end # Jump to the end, which will make RARS stop.

fib:
  addi sp, sp, -16        # adjust stack for 4 words
  sw x18, 12(sp)          # save x18-contents for later
  sw ra, 8(sp)            # save return address
  sw x10, 4(sp)           # save contents of argument n
  addi x18, x0, 2         # save 2 in x18 for later comparisons
  
  blt x10, x18, fib_base  # if n < 2, go to fib_base

  addi x10, x10, -1       # argument n is decreased by 1
  jal ra, fib             # call fib with x10 = (n-1), write result to x10 
  sw x10, 0(sp)           # save fib(n-1) in stack
  lw x10, 4(sp)           # load value of n into x10
  addi x10, x10, -2       # x10 is filled with n-2
  jal ra, fib             # call fib with x10 = (n-2), write result to x10
  lw x5, 0(sp)            # retrieve fib(n-1) from stack into t0
  add x10, x10, x5       # puts the result fib(n-1)+fib(n-2) in x10
  jal x0, fib_return
  
fib_return:
  # at this point, x10 should be loaded with the correct value.

  lw ra, 8(sp)            # restore the return address
  lw x18, 12(sp)          # restore the value of x18
  addi sp, sp, 16         # adjust stack pointer to pop 5 items
  jalr x0, 0(ra)          # return to the caller

fib_base:
  # if this is reached, then n < 2. We need to just return 1.
  addi x10, x0, 1       # return 1 in output-register x10
  jal ra, fib_return      # go to return-conditional

end: