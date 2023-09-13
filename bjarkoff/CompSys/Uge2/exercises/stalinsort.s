# We are to write a simple function, stalinsort, that takes two parameters in a0 and a1, 
# and writes result in a0. The parameters are an int array in a0 and a length in a1.
# the algorithm ignores every element that is not at least as great as its predecessor,
# it moves up every element that is greater than predecessor and returns the new
# length of the array at the end.

# thoughts: We should save the original length and orignal array, to be able to move
# up and down through those

.data
array:
	.word 2
	.word 1
	.word 0
	.word 4
	.word 5
	.word 2

.text
init:
	la a0, array
	li a1, 6
	jal ra, stalinsort
	jal zero, end

stalinsort:
  addi sp, sp, -20 # five words to be saved
  sw ra, 0(sp) # save return address
  sw s0, 4(sp) # save s0
  sw s1, 8(sp)  # save s1
  sw s2, 12(sp) # save s2 for an iterator
  sw s3, 16(sp)

  mv s0, a0 # int array pointer in s0
  mv s1, a1 # length in s1
  lw s2, 0(s0) # load first word from array into s2
  li s3, 1 # set iterator i to 1
  beq s1, x0, return_nzero # if n == 0, return 0.

  li t0, 1 # set iterator j to 1

stalin_loop:
  bge t0, s1, return_loop # if j >= n, return from loop
  slli t0, t0, 2 # multiply j by 4 to find next item in array
  add t3, s0, t0 # use t3 for the pointer for A[j]
  lw t1, 0(t3) # load A[j] into t1
  blt t1, s2, loop_condition_not_met # if A[j] < prev, next things should be skipped:

  mv s2, t1 # set prev = A[j]
  slli t2, s3, 2 # multiply i by 4, put result in t2
  add t3, t2, s0 # use t3 for the pointer A[i]
  sw s2, 0(t3) # save prev in address A[i]
  addi s3, s3, 1 # iterator i incremented by 1

loop_condition_not_met:
  srli t0, t0, 2 # divide j by 4 to make it a normal iterator again
  addi t0, t0, 1 # increment iterator j 
  jal zero, stalin_loop # force-go to stalin_loop again


return_loop:
  mv a0, s3
  lw s3, 16(sp)
  lw s2, 12(sp)
  lw s1, 8(sp)
  lw s0, 4(sp)
  lw ra, 0(sp)
  jalr x0, 0(ra) # return to caller

return_nzero:
  li a0, 0
  lw s3, 16(sp)
  lw s2, 12(sp)
  lw s1, 8(sp)
  lw s0, 4(sp)
  lw ra, 0(sp)
  jalr x0, 0(ra) # return to caller

end:
