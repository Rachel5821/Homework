# Supplier Management System for Medium and Large Businesses

This repository contains the implementation of **Part IV** of a multi-part technical assignment.

The assignment was structured in four distinct parts, each designed to evaluate different capabilities: data processing, relational reasoning, hardware understanding, and system development.  
Part IV builds on the skills demonstrated in the previous parts and presents an applied, full-stack solution to a real-world problem.

## Overview of Assignment Structure

### Part I – Data Processing and Analysis

The goal of this part was to process large-scale textual and tabular datasets.

- **Section A**: Analyze a large log file to identify the most frequent error codes.  
- **Section B**: Compute hourly averages from time-series data stored in CSV and Parquet formats, including a consideration of real-time data streams.

**Technology Used**:  
Implemented in **Python** using **Jupyter Notebook**. Data was processed using libraries such as `pandas` and `collections`.

---

### Part II – Family Relationship Inference

This part focused on constructing relationship mappings from raw data records.

- A relational structure of individuals was used to derive immediate family relations (e.g., parent, sibling, spouse).  
- Additional logic ensured that spouse relationships were reciprocal, even when originally only partially defined.

**Technology Used**:  
Implemented in **Python** using **Jupyter Notebook**, with emphasis on data structuring, mapping, and logical inference.

---

### Part III – Hardware Concepts and Data Modeling

This part combined hardware-related theoretical analysis with SQL-based data modeling.

- Provided an engineering analysis of how a remote control communicates with an air conditioner, addressing components, signal methods, and encoding strategies.  
- Modeled and queried family data using structured relational tables.

**Technology Used**:  
Implemented using **SQL Server**, including the design of normalized tables and relational queries.

---

### Part IV – Supplier Management System (This Repository)

The fourth and final part of the assignment focused on the development of a supplier management platform for a neighborhood store. The system includes both client and server components:

- **Supplier-side (Client)**:  
  - Register and log in  
  - View orders issued by the store owner  
  - Approve orders, changing their status accordingly

- **Store-owner-side (Server)**:  
  - Create and manage orders  
  - Track the status of all orders  
  - Confirm receipt of orders and notify suppliers

- **Extended Functionality (Bonus)**:  
  - Inventory tracking via integration with a point-of-sale system  
  - Automatic reordering based on minimum stock thresholds  
  - Supplier selection based on pricing

**Technologies Used**:  
- **Backend**: Implemented in **.NET (C#)**, using principles of **Dependency Injection (DI)** for modularity and testability  
- **Frontend**: Developed in **React**  
- **Database**: Implemented using **SQL Server**  
- **Communication**: RESTful APIs enabling interaction between client and server

---

## Purpose of This Repository

This repository presents the implementation of Part IV.  
It demonstrates the ability to design and build a complete, maintainable system that integrates user interfaces, business logic, and data management. The system reflects both practical software engineering practices and the conceptual understanding established in earlier parts of the assignment.
