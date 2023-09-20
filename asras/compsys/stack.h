#ifndef STACK_H
#define STACK_H

#include <string.h> // for memcpy

typedef struct Stack {
    struct Stack *previous;
} Stack;

Stack stack_new(void) {
    Stack stack = { NULL };
    return stack;
}


void stack_push(Stack **pp_stack, Stack *element) {
    Stack *stack = *pp_stack;
    ((Stack *)element)->previous = stack;
    *pp_stack = element;
}

void * stack_pop(Stack **pp_stack) {
    // Find previous node
    Stack *stack = *pp_stack;
    Stack *previous = stack->previous;
    // Detach head from stack
    Stack *current = stack;
    current->previous = NULL;
    // Update stack to point to previous
    (*(Stack **)pp_stack) = previous;
    return current;
}

int stack_is_empty(Stack *stack) {
    return stack->previous == NULL;
}


#define stack_push_fancy(pp_stack, value) stack_push_fancy_impl(pp_stack, sizeof(value), &value)
void stack_push_fancy_impl(Stack **pp_stack, int datasize, void *data) {
    Stack *element = malloc(sizeof(Stack) + datasize);
    memcpy(element + 1, data, datasize);
    stack_push(pp_stack, element);
}
#endif // !STACK_H
