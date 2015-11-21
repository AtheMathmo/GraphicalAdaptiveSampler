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

The FactorNode and VariableNode classes are abstract currently. I am leaning towards having the FactorNode be a wrapper for the factor. Factors must be implemented on a per use basis (e.g. factor for sum of Gaussians). It makes sense to keep the factors and the graph/algorithm separate.

## Belief Propagation

Some details are still to be decided in terms of how the algorithm will be run. But the basic idea is to get the root node and work out to map the tree. As we do so we will build a LIFO stack and a FIFO queue which we will use later to schedule the message passing.

### Details still to be decided

1. Where should the messages be stored? One option is to store the message values within the nodes themselves - along with the neighbour they are being passed towards.
