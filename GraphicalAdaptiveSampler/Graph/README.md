# Belief Propagation

This document describes my implementation of belief propagation. At the time of writing it is still a work in progress but I think most of the details have been worked out. If you happen to be reading this and have suggestions please get in touch!

## Factor Graphs

I am implementing belief propagation using factor graphs. The factor graphs consist of three classes:

- FactorGraph
- FactorNode
- VariableNode

Each node is reponsible for tracking it's neighbours and also notes who its parent/child is. Each factor has a single child and each variable a single parent, corresponding to p(v | a(v)), p is the factor which is the parent of v.
_It isn't necessary to note the parents and children and so I may exclude it later for simplicity._

The FactorGraph class records the root node and also stores all of the variable and factor nodes.

The FactorNode and VariableNode classes are wrappers for the factor and variables respectively. Factors must be implemented on a per use basis (e.g. factor for sum of Gaussians). It makes sense to keep the factors and the graph/algorithm separate.

## Belief Propagation

Some details are still to be decided in terms of how the algorithm will be run. But the basic idea is to get the root node and work out to map the tree. As we do so we will build a LIFO stack and a FIFO queue which we will use later to schedule the message passing.

The messages are stored within the Nodes - a dictionary maps each neighbour to an outbound message. This is probably not the best way to handle this as the graphical model and the algorithm are closely linked.

### Details still to be decided

1. How to handle message computation. Right now I am looking at delegate functions and having the message class be a wrapper for this function. The function will provide methods to do summation and products. *I'm sure there's a better way to handle this.*

## TODO:

1. Implement message computation.


