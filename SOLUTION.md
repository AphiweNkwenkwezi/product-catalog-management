# SOLUTION.md

Design Decisions & Trade-offs

------------------------------------------------------------------------

# 1. Architectural Approach

The backend follows Clean Architecture principles with clear separation:

-   Domain: Core entities and interfaces
-   Application: Business logic, filtering, search engine
-   Infrastructure: EF Core + in-memory repositories
-   API: Controllers, middleware, serialization

The design prioritizes extensibility, testability, and clarity.

------------------------------------------------------------------------

# 2. Generic Repository`<T>`{=html}

A custom Repository`<T>`{=html} base class was implemented using
generics and interfaces to demonstrate:

-   Reusability
-   Dependency inversion
-   Clean abstraction over data access

One repository uses pure in-memory collections (List) to
satisfy the constraint of avoiding EF for at least one data store.

------------------------------------------------------------------------

# 3. ProductSearchEngine (Core C# Only)

This class was implemented using only the .NET Base Class Library.

Features: - Fuzzy matching using Levenshtein distance - Weighted scoring
across multiple fields - Generic implementation for reuse - Efficient
in-memory search - Dictionary-based caching layer

Trade-off: A more advanced indexing strategy (e.g., Trie) was avoided to
keep the implementation concise within time constraints.

------------------------------------------------------------------------

# 4. Caching Strategy

Search results are cached using Dictionary\<TKey, TValue\>.

This improves repeated query performance while keeping implementation
simple.

Trade-off: No expiration strategy was implemented due to scope
constraints.

------------------------------------------------------------------------

# 5. Category Tree

Hierarchical categories are built using recursive tree construction.

This supports unlimited depth while keeping API responses clean.

------------------------------------------------------------------------

# 6. Custom Middleware

A middleware was written from scratch (not using built-in helpers) to
demonstrate:

-   Understanding of request pipeline
-   Custom logging / error handling
-   Manual response manipulation

------------------------------------------------------------------------

# 7. Manual Model Binding

One controller action demonstrates manual extraction and parsing of
request data to show understanding of model binding internals.

------------------------------------------------------------------------

# 8. Angular Architecture

Frontend uses:

-   Standalone components
-   Reactive forms with validation
-   RxJS for API interaction
-   Strong TypeScript typing
-   Clean service abstraction
-   Unit test for product service

Trade-off: UI styling kept minimal to focus on functionality and
architecture.

------------------------------------------------------------------------

# 9. Scalability Considerations

The current design allows future extension with:

-   Pagination optimization
-   Persistent caching
-   Database indexing
-   CQRS with MediatR
-   Authentication/Authorization
-   Microservice extraction

------------------------------------------------------------------------

# 10. Conclusion

The goal was not just to meet the requirements but to demonstrate:

-   Strong core C# knowledge
-   Understanding of generics and LINQ
-   Clean architectural patterns
-   Practical Angular expertise
-   Thoughtful trade-offs within time constraints
