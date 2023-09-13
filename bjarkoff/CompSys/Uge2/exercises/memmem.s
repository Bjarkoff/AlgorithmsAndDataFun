# We use the memeq-function as a helper function.
# memeq can test whether two arrays are equal, so I think
# this should not be too bad.
# we accept four parameters, haystack, hastacklen, needle and needlelen.
# loaded in a0-a3, and result should be written to a0.



.data
haystack:
	.byte 1
	.byte 2
	.byte 3
	.byte 4
	.byte 5
	.byte 6
	
needle1:
	.byte 4
	.byte 5
	.byte 7

needle2:
	.byte 1
	.byte 2
	.byte 3

needle3:
	.byte 4
	.byte 5
	.byte 6

.text
init:
	la a0, haystack
	la a1, needle2
	li a2, 6
	li a3, 3
	jal ra, memmem
	jal zero, end




memeq:
  addi sp, sp, -12 # make room for 3 words in stack
  sw ra, 8(sp) # saves the return address of caller
  sw a0, 4(sp) # save pointer-address for first char-array 
  sw a1, 0(sp) # save pointer-address for second char-array
  beq a2, x0, return_true_memeq # if a2 has become 0, program must return true
  # otherwise, we go to compare contents of arrays below:

  lw t0, 4(sp) # load current pointer for first array
  lw t1, 0(sp) # load current pointer for second array

  lb t2, 0(t0) # load the word value pointed by t0 into t2
  lb t3, 0(t1) # load the word value pointed by t1 into t3

  bne t2, t3, return_false_memeq # compare the word values, not the pointers

  addi a2, a2, -1 # if no equality, first we let n decrement by 1

  addi a0, a0, 1 # next we move to next byte in first array
  addi a1, a1, 1 # and in second array

  beq x0, x0, memeq # and finally we go back to memeq again

return_false_memeq:
  addi a0, x0, 0
  lw ra, 8(sp)
  addi sp, sp, 12
  jalr x0, 0(ra) #return to caller

return_true_memeq:
  addi a0, x0, 1
  lw ra, 8(sp)
  addi sp, sp, 12
  jalr x0, 0(ra) #return to caller








memmem:
  
  addi sp, sp, -24 # make room for stuff helper
  sw ra, 0(sp)
  sw s0, 4(sp)
  sw s1, 8(sp)
  sw s2, 12(sp)
  sw s3, 16(sp)
  sw s4, 20(sp)

  mv s0, a0 # haystack in s0
  mv s1, a1 # needle in s1
  mv s2, a2 # haystacklen in s2
  mv s3, a3 # needlelen in s3

  li s4, 0 # this is a value, that addresses how far in haystack we have moved.

loop:

  sub t0, s2, s3
  blt t0, s4, return_false
  jal ra, memeq
  addi a0, a0, -1 # now a0 is 0 if there was success
  beq a0, zero, return_true # if memeq returned 1, go to "true", else:
  addi s4, s4, 1 # we add 1 to how far we are looking into haystack
  jal zero, loop # we call the function again

return_true:
  add a0, s4, s0
  lw ra, 0(sp)
  lw s0, 4(sp)
  lw s1, 8(sp)
  lw s2, 12(sp)
  lw s3, 16(sp)
  lw s4, 20(sp)
  addi sp, sp, 24 # restore stack
  jalr x0, 0(ra) # return to caller


return_false:
  li a0, 0
  lw ra, 0(sp)
  lw s0, 4(sp)
  lw s1, 8(sp)
  lw s2, 12(sp)
  lw s3, 16(sp)
  lw s4, 20(sp)
  addi sp, sp, 24 # restore stack
  jalr x0, 0(ra) # return to caller






end: 
