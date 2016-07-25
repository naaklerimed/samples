#include <iostream>
#include <sstream>
#include <fstream>
#include <stdio.h>
#include <string>
#include <vector>
using namespace std;


/*
Picks 20 random data points
*/
void randomTwenty(){
	int randomJumper = rand() % 150;
	ifstream infile("adult.dataCleaned.txt");
	ofstream outfile("random.txt");
	string line;
	int loopcounter = 0;
	int lineCount = 0;
	
		while (getline(infile, line)){
			if (lineCount < 10){
			if (loopcounter % randomJumper == 0){
				outfile << line << endl;
				lineCount++;
			}
			loopcounter++;
		}
	}
		
}


void split(const string& s, char c,vector<string>& v) {
	string::size_type i = 0;
	string::size_type j = s.find(c);

	while (j != string::npos) {
		v.push_back(s.substr(i, j - i));
		i = ++j;
		j = s.find(c, j);

		if (j == string::npos)
			v.push_back(s.substr(i, s.length()));
	}
}

/*
performs bayesian classifier operations
*/
void bayesianClassifier(int percentage){
	string percentageToStr = to_string(percentage);
	ifstream random("random.txt");
	ifstream samplePositive(percentageToStr+"sampleP.txt");
	ifstream sampleNegative(percentageToStr+"sampleN.txt");
	ifstream sample(percentageToStr + "sample.txt");
	ifstream sample2(percentageToStr + "sample.txt");
	string fullLine;
	int positiveLineCount = 0, negativeLineCount = 0, totalCount=0;
	while (getline(sample, fullLine)){
		if (fullLine.find("<=50K") != std::string::npos) {
			positiveLineCount++;
			totalCount++;
		}
		else if (fullLine.find(">50K") != std::string::npos){
			negativeLineCount++;
			totalCount++;
		}
	}
	double posPer = 0.0;
	posPer = (double)(positiveLineCount / totalCount);
	double negPer = 0.0;
	negPer = (double)(negativeLineCount / totalCount);
	string line, sampleLine;
	string statArray[20], compareArray[20];
	int currentIndice = 0;
	string currentAttribute;
	int sampleLineCount = 0;
	int sampleMatch = 0;
	int foundTrue = 0;
	double totalPercentageP = 1.0, totalPercentageN = 1.0;
	double currentPercentageP = 1.0, currentPercentageN = 1.0;
	vector<string> attributeStat, sampleStat;
	while (getline(random, line)){
		if (currentIndice < 10){
			if (line.find("<=50K") != std::string::npos) {
				statArray[currentIndice] = "<=50K";
				currentIndice++;
			}
			else if (line.find(">50K") != std::string::npos){
				statArray[currentIndice] = ">50K";
				currentIndice++;
			}
		}
	}
	random.close();
	ifstream random2("random.txt");
	int count = 0;
	while (getline(random2, line)){

		attributeStat.clear();
		
		split(line, ', ', attributeStat);
		while (getline(samplePositive, sampleLine)){
			for (int i = 0; i < attributeStat.size(); i++){
				sampleLineCount++;
				sampleStat.clear();
				split(sampleLine, ', ', sampleStat);
				if (sampleStat[i] == attributeStat[i]){
					sampleMatch++;
				}
			}
				if (sampleMatch != 0){
					currentPercentageP = (double)sampleMatch / positiveLineCount;
					sampleMatch = 0;
					totalPercentageP = totalPercentageP * currentPercentageP;
				}
			
		}
		while (getline(sampleNegative, sampleLine)){
			for (int i = 0; i < attributeStat.size(); i++){
				sampleLineCount++;
				sampleStat.clear();
				split(sampleLine, ', ', sampleStat);
				if (sampleStat[i] == attributeStat[i]){
					sampleMatch++;
				}
			}
				if (sampleMatch != 0){
					currentPercentageN = (double)sampleMatch / negativeLineCount;
					sampleMatch = 0;
					totalPercentageN = totalPercentageN * currentPercentageN;
				}
			
			
		}
		if (count<10)
			if (totalPercentageP*posPer >= totalPercentageN*negPer){
				compareArray[count] = "<=50K";
			}
			else{
				compareArray[count] = ">50K";
			}
			count++;
		}
	
	for (int i = 0; i < 10; i++){
		if (compareArray[i] == statArray[i]){
			foundTrue++;
		}
	}

	double accuracy = (double)(foundTrue * 10 / 100.0);
	cout << accuracy << endl;
	
}


/*
creates data samples with given percentage
*/
void dataSampler(int percentage){
	string percentageToStr = to_string(percentage);
	ifstream inFile("adult.dataCleaned.txt");
	int positiveLineCount = 0;
	int negativeLineCount = 0;
	ofstream outFile(percentageToStr + "sample.txt");
	ofstream outFilePositive(percentageToStr + "sampleP.txt");
	ofstream outFileNegative(percentageToStr + "sampleN.txt");
	string line, positiveLine, negativeLine;
	while (getline(inFile, line)){
		istringstream iss(line);
		
		if (line.find("<=50K") != std::string::npos) {
			negativeLineCount++;
			outFileNegative << line << endl;
		}
		else if (line.find(">50K") != std::string::npos){
			positiveLineCount++;
			outFilePositive << line << endl;
		}
	}
	ifstream inFilePositive(percentageToStr + "sampleP.txt");
	ifstream inFileNegative(percentageToStr + "sampleN.txt");
	int outLineNumberP = (int)((positiveLineCount*percentage / 100));
	int outLineNumberN = (int)((negativeLineCount * percentage / 100));

	int addedLineNum = 0;
	int loopCounter = 0;
	while (getline(inFilePositive, positiveLine)){
		if (percentage != 30){
			if (loopCounter % (int)(positiveLineCount / (positiveLineCount*percentage / 100)) == 0){
				outFile << positiveLine << endl;
			}
		}
		else{
			if (loopCounter % (int)(positiveLineCount / (positiveLineCount*percentage / 100)) == 0 && addedLineNum <= outLineNumberP ){
				outFile << positiveLine << endl;
				addedLineNum++;
			}
		}
		loopCounter++;
	}
	addedLineNum = 0;
	loopCounter = 0;
	while (getline(inFileNegative, negativeLine)){
		if (percentage != 30){
			if (loopCounter % (int)(negativeLineCount / (negativeLineCount*percentage / 100)) == 0){
				outFile << negativeLine << endl;
			}
		}
		else{
			if (loopCounter % (int)(negativeLineCount / (negativeLineCount*percentage / 100)) == 0 && addedLineNum <= outLineNumberN){
				outFile << negativeLine << endl;
				addedLineNum++;
			}
		}
		loopCounter++;
	}



}

/*
cleans incoming data, ignores lines with missing values
*/
void dataCleaner(string fileName){
	ifstream inFile(fileName);
	ofstream outFile("adult.dataCleaned.txt", std::ofstream::out | std::ofstream::app);

	string line;
	bool found = false;
	while (getline(inFile, line)) {
		
		istringstream iss(line);
		for (int i = 0; i < line.size(); i++){
			if (line.at(i) == '?'){
				found = true;
			}
		}
			if (!found){
				outFile << line << endl;
			}
			else{ 
				found = false;
				
			}
		
	}

	inFile.close();
	outFile.close();
}

int main() {
	string filename, answer;
	bool finished = false;
	while (!finished){
		cout << "Enter dataset file name ( Enter adult.data ) : ";
		cin >> filename;
		dataCleaner(filename);
		cout << "Do you have other files? (Y OR N) :";
		cin >> answer;
		if (answer == "Y" || answer == "y")
			finished = false;
		else
			finished = true;
	}
	bool needAnother = true;
	int percentage;
	string cleanedFileName, samplingAnswer;
	while (needAnother){
		cout << "Enter the percentage for stratified sampling( 10-30-50 ): ";
		cin >> percentage;
		
		dataSampler(percentage);
		cout << "Do you need another sampling operation? (Y OR N) :";
		cin >> samplingAnswer;
		if (samplingAnswer == "Y" || samplingAnswer == "y")
			needAnother = true;
		else
			needAnother = false;
	}
	needAnother = true;
	randomTwenty();
	string classificationAnswer;
	int perChoice;
	cout << "20 random data generated!" << endl;
	while (needAnother){
		cout << "Enter the sampling percentage for Bayesian Classification ( 10-30-50 ): ";
		cin >> perChoice;
		bayesianClassifier(perChoice);
		cout << "Do you need more classification operation? ( Y OR N ) : ";
		cin >> classificationAnswer;
		if (classificationAnswer == "Y" || classificationAnswer == "y"){
			needAnother = true;
		}
		else{
			needAnother = false;
		}
	}
	

	
	return 0;
}

