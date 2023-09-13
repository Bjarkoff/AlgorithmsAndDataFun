# we have to write instructions for f = g + ( h - 5), where
# f,g and h are stored in adress x5, x6 and x7 respectively
addi x7, x7, -5 # x7 = x7 - 5
add x5, x6, x7 # x5 = x6 + x7
