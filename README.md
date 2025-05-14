# Thermal Conductivity Testing Application

## Overview
This application measures and analyzes the thermal conductivity of materials using a steady-state heat flow method. It interfaces with external hardware that provides temperature readings, processes the data, and calculates the thermal conductivity value in real-time.

## Features
* Real-time thermal conductivity measurements
* Data logging to CSV format
* Serial connection to temperature sensing hardware
* Calculation of average thermal conductivity
* Support for various sample dimensions

## Requirements

### Hardware
* Temperature sensing apparatus with serial output capability
* Serial port connection (COM port)
* Material samples of known dimensions

### Software
* Windows OS
* .NET Framework 2.0 or higher

## Installation
1. Clone this repository or download the ZIP file
2. Open the solution in Visual Studio
3. Build the solution
4. Run the application

## Usage

### Setup
1. Prepare your material sample and measure its length and diameter
2. Connect your temperature sensing hardware to your computer via serial port
3. Launch the application

### Measurement Process
1. Enter the sample length (m) in the "Length" field
2. Enter the sample diameter (m) in the "Diameter" field
3. Enter the voltage (V) being applied to the heating element
4. Enter a name for the material being tested (optional)
5. Click "Connect" to begin data acquisition
6. The application will display real-time measurements in the list box
7. When finished, click "Disconnect" to stop data acquisition and see the average thermal conductivity

### Data Export
All measurement data is automatically saved to "Thermal Conductivity Data.csv" in the application directory. This file includes:
* Hot temperature readings
* Cold temperature readings
* Temperature differences
* Calculated thermal conductivity values
* Timestamps

## Theory of Operation
The application uses the steady-state heat flow method to calculate thermal conductivity. A known power (P = V²/R) is applied to one end of a sample, and the temperature gradient across the sample is measured. The thermal conductivity (k) is calculated using:

k = (Q × L) / (A × ΔT) × c

Where:
* Q = Power input (W)
* L = Sample length (m)
* A = Cross-sectional area (m²)
* ΔT = Temperature difference (K)
* c = Calibration constant

## Troubleshooting
* If the application fails to connect to the hardware, verify the correct COM port is selected
* Ensure the hardware is sending data in the format "hot_temp;cold_temp"
* Verify all dimensions are entered in meters
* Check that the voltage value is accurate

## Contributors
* Prof. I. K. Adegun
* J. K. Ishola
