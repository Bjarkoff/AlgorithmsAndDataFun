// Disclaimer: I'm not saying this is optimal, this is just to prove that it's possible to make a stack in this way.
#include <stdio.h>
#include <stdlib.h>

#include "stack.h"


// The stack works for any type as long as the first field is a pointer to the previous node.
typedef struct IntNode {
    Stack *previous;
    int value;
} IntNode;


typedef struct FloatNode {
    Stack *previous;
    float hello;
} FloatNode;




int main(void) {
    {
        // Initialize stack.
        // No specific function for this because the stack is supposed to work with arbitrary data types.
        IntNode *stack = calloc(1, sizeof(IntNode));
        stack->value = 123;

        // Make another value and push
        IntNode other = { NULL, 456 };
        stack_push(&stack, &other);

        // Make another value and push
        IntNode otheragain = { NULL, 789 };
        stack_push(&stack, &otheragain);

        // We can even get fancy 
        int othervalue = 101;
        stack_push_fancy(&stack, othervalue);

        // Print out
        printf("Ints:\n");
        while (!stack_is_empty(&stack)) {
            IntNode *node = stack_pop(&stack);
            printf("%d\n", node->value);
        }
    }

    {
        // Init
        FloatNode *stack = malloc(sizeof(FloatNode));
        stack->previous = NULL;
        stack->hello = 1.23;

        // Push new
        FloatNode other = { NULL, 4.56 };
        stack_push(&stack, &other);

        // Push new
        FloatNode otheragain = { NULL, 7.89 };
        stack_push(&stack, &otheragain);

        // Print out
        printf("Floats:\n");
        while (!stack_is_empty(&stack)) {
            FloatNode *node = stack_pop(&stack);
            printf("%f\n", node->hello);
        }
    }


    {
        // We get more control over memory layout with this method
        // Initialize many nodes
        IntNode *nodes = calloc(3, sizeof(IntNode)); // Nodes are adjacent in memory
        nodes[0].value = 1;
        nodes[1].value = 2;
        nodes[2].value = 3;
        IntNode *stack = &nodes[2]; // Stack points to the last node
        // We don't have to push in order, although if we don't we might lose memory locality
        // Other the hand, we are only using this type of struct if the data is not laid out nicely in the first place
        stack_push(&stack, &nodes[0]);
        stack_push(&stack, &nodes[1]);

        // Print out
        printf("Ints again:\n");
        while (!stack_is_empty(&stack)) {
            IntNode *node = stack_pop(&stack);
            printf("%d\n", node->value);
        }
    }

    return 0;
}


