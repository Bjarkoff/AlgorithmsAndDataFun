# We are to implement a simplified version of equality for two
# arrays of char. 

init:
  addi a2, x0, 3 # load n = 3, so that we test the first three items of the two arrays
  addi sp, sp, -12 # add for 12 extra words in sp
  addi a1, sp, 7 # set the pointer for second array to byte nr. 7 in stack
  addi a0, sp, 0 # set the pointer for first array to first word in stack

  sb x0, 0(a0) # initializing first array
  sb x0, 1(a0)
  sb x0, 2(a0)

  sb x0, 0(a1) # initializing second array
  sb x0, 1(a1)
  sb x0, 2(a1)

  # sb a1, 2(sp) # un-commenting this line should result in non-equality of the two arrays
  # if above line is not un-commented, the two arrays should be equal (both all zeros)

  jal ra, memeq  # Call fact procedure
  jal zero, end # Jump to the end, which will make RARS stop.

memeq:
  addi sp, sp, -12 # make room for 3 words in stack
  sw ra, 8(sp) # saves the return address of caller
  sw a0, 4(sp) # save pointer-address for first char-array 
  sw a1, 0(sp) # save pointer-address for second char-array
  beq a2, x0, return_true # if a2 has become 0, program must return true
  # otherwise, we go to compare contents of arrays below:

  lw t0, 4(sp) # load current pointer for first array
  lw t1, 0(sp) # load current pointer for second array

  lb t2, 0(t0) # load the word value pointed by t0 into t2
  lb t3, 0(t1) # load the word value pointed by t1 into t3

  bne t2, t3, return_false # compare the word values, not the pointers

  addi a2, a2, -1 # if no equality, first we let n decrement by 1

  addi a0, a0, 1 # next we move to next byte in first array
  addi a1, a1, 1 # and in second array

  beq x0, x0, memeq # and finally we go back to memeq again

return_false:
  addi a0, x0, 0
  lw ra, 8(sp)
  addi sp, sp, 12
  jalr x0, 0(ra) #return to caller

return_true:
  addi a0, x0, 1
  lw ra, 8(sp)
  addi sp, sp, 12
  jalr x0, 0(ra) #return to caller

end:
