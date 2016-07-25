#include <iostream>
#include <string>
#include <fstream>
#include <sstream>
#include <vector>
#include <algorithm>
#include <time.h>
#include <math.h>
#include <cmath>
using namespace std;
typedef vector<double> labelVector;
typedef vector<labelVector> dataVector ;

//a string split function for the .data file splitting
vector<double> &split(const string &s, char delim, vector<double> &elems) {
	stringstream ss(s);
	string item;
	char *pEnd;	
	while (getline(ss, item, delim)) {
		if (strtof(item.c_str(), &pEnd) != 0.000000000){
			elems.push_back(strtof(item.c_str(), &pEnd));
		}
	}
	return elems;
}
double centroidFinder(dataVector& centroids, dataVector& data, int h, bool& centroidsFound, int (&clusterSizes)[6]){
	double dist = 0.0;
	labelVector tempDistances;
	dataVector distances;
	int firstCount = 0, secondCount = 0, thirdCount = 0, fourthCount = 0, fifthCount = 0, sixthCount = 0;
	for (int c = 0; c != centroids.size(); c++){
		for (int d = 0; d != data.size(); d++){
			for (int i = 0; i < h; i++){
				dist = dist + ((centroids.at(c).at(i) - data.at(d).at(i)) * (centroids.at(c).at(i) - data.at(d).at(i)));
			}
			dist = sqrt(dist);
			tempDistances.push_back(dist);
			dist = 0.0;

		}
		distances.push_back(tempDistances);
		tempDistances.clear();
	}
	//find distances
	double distanceCheck[6];
	dataVector first, second, third, fourth, fifth, sixth;
	for (int i = 0; i < data.size(); i++){
		distanceCheck[0] = (distances.at(0).at(i));
		distanceCheck[1] = (distances.at(1).at(i));
		distanceCheck[2] = (distances.at(2).at(i));
		distanceCheck[3] = (distances.at(3).at(i));
		distanceCheck[4] = (distances.at(4).at(i));
		distanceCheck[5] = (distances.at(5).at(i));
		if (min_element(distanceCheck, distanceCheck + 6) == &distanceCheck[0]){
			firstCount++;
			first.push_back(data.at(i));
		}
		else if (min_element(distanceCheck, distanceCheck + 6) == &distanceCheck[1]){
			secondCount++;
			second.push_back(data.at(i));
		}
		else if (min_element(distanceCheck, distanceCheck + 6) == &distanceCheck[2]){
			thirdCount++;
			third.push_back(data.at(i));
		}
		else if (min_element(distanceCheck, distanceCheck + 6) == &distanceCheck[3]){
			fourthCount++;
			fourth.push_back(data.at(i));
		}
		else if (min_element(distanceCheck, distanceCheck + 6) == &distanceCheck[4]){
			fifthCount++;
			fifth.push_back(data.at(i));
		}
		else if (min_element(distanceCheck, distanceCheck + 6) == &distanceCheck[5]){
			sixthCount++;
			sixth.push_back(data.at(i));
		}
	}
	int currentFirst = clusterSizes[0];
	int currentSecond = clusterSizes[1];
	int currentThird = clusterSizes[2];
	int currentFourth = clusterSizes[3];
	int currentFifth = clusterSizes[4];
	int currentSixth = clusterSizes[5];
	clusterSizes[0] = first.size();
	clusterSizes[1] = second.size();
	clusterSizes[2] = third.size();
	clusterSizes[3] = fourth.size();
	clusterSizes[4] = fifth.size();
	clusterSizes[5] = sixth.size();
	//find centroids
	if (currentFirst == clusterSizes[0] && currentSecond == clusterSizes[1] && currentThird == clusterSizes[2] && currentFourth == clusterSizes[3] && currentFifth == clusterSizes[4] && currentSixth == clusterSizes[5]){
		centroidsFound = true;
	}
	double current = 0.0;
	if (firstCount != 0){
		for (int i = 0; i < h; i++){
			for (int j = 0; j < firstCount; j++){
				current = current + first.at(j).at(i);
			}
			centroids.at(0).at(i) = current / firstCount;
			current = 0.0;
		}
	}
	if (secondCount != 0){
		for (int i = 0; i < h; i++){
			for (int j = 0; j < secondCount; j++){
				current = current + second.at(j).at(i);
			}
			centroids.at(0).at(i) = current / secondCount;
			current = 0.0;
		}
	}
	if (thirdCount != 0){
		for (int i = 0; i < h; i++){
			for (int j = 0; j < thirdCount; j++){
				current = current + third.at(j).at(i);
			}
			centroids.at(0).at(i) = current / thirdCount;
			current = 0.0;
		}
	}
	if (fourthCount != 0){
		for (int i = 0; i < h; i++){
			for (int j = 0; j < fourthCount; j++){
				current = current + fourth.at(j).at(i);
			}
			centroids.at(0).at(i) = current / fourthCount;
			current = 0.0;
		}
	}
	if (fifthCount != 0){
		for (int i = 0; i < h; i++){
			for (int j = 0; j < fifthCount; j++){
				current = current + fifth.at(j).at(i);
			}
			centroids.at(0).at(i) = current / fifthCount;
			current = 0.0;
		}
	}
	if (sixthCount != 0){
		for (int i = 0; i < h; i++){
			for (int j = 0; j < sixthCount; j++){
				current = current + sixth.at(j).at(i);
			}
			centroids.at(0).at(i) = current / sixthCount;
			current = 0.0;
		}
	}
	dataVector dataToCheck;
	for (int i = 0; i < first.size(); i++){
		dataToCheck.push_back(first.at(i));
	}
	for (int i = 0; i < second.size(); i++){
		dataToCheck.push_back(second.at(i));
	}
	for (int i = 0; i < third.size(); i++){
		dataToCheck.push_back(third.at(i));
	}
	for (int i = 0; i < fourth.size(); i++){
		dataToCheck.push_back(fourth.at(i));
	}
	for (int i = 0; i < fifth.size(); i++){
		dataToCheck.push_back(fifth.at(i));
	}
	for (int i = 0; i < sixth.size(); i++){
		dataToCheck.push_back(sixth.at(i));
	}
	int currentMatchNum = 0;
	int firstMatch = 0, secondMatch = 0, thirdMatch = 0, fourthMatch = 0, fifthMatch = 0, sixthMatch = 0;
	int totalMatches[6];
	int max = 0;
	/////////////FIRST//////////////
	for (int i = 0; i < first.size(); i++){
		for (int j = 0; j < 100; j++){
			if (first.at(i) == data.at(j)){
				firstMatch++;
			}
		}
		for (int j = 100; j < 200; j++){
			if (first.at(i) == data.at(j)){
				secondMatch++;
			}
		}
		for (int j = 200; j < 300; j++){
			if (first.at(i) == data.at(j)){
			thirdMatch++;
			}
		}
		for (int j = 300; j < 400; j++){
			if (first.at(i) == data.at(j)){
				fourthMatch++;
			}
		}
		for (int j = 400; j < 500; j++){
			if (first.at(i) == data.at(j)){
				fifthMatch++;
			}
		}
		for (int j = 0; j < 100; j++){
			if (first.at(i) == data.at(j)){
				sixthMatch++;
			}
		}
	}
	totalMatches[0] = firstMatch;
	totalMatches[1] = secondMatch;
	totalMatches[2] = thirdMatch;
	totalMatches[3] = fourthMatch;
	totalMatches[4] = fifthMatch;
	totalMatches[5] = sixthMatch;
	for (int i = 0; i < 6; i++){
		if (max <= totalMatches[i])
			max = totalMatches[i];
	}
	currentMatchNum = currentMatchNum + max;
	firstMatch = 0;
	secondMatch = 0;
	thirdMatch = 0;
	fourthMatch = 0;
	fifthMatch = 0;
	sixthMatch = 0;
	////////////////SECOND/////////////////////
	for (int i = 0; i < second.size(); i++){
		for (int j = 0; j < 100; j++){
			if (second.at(i) == data.at(j)){
				firstMatch++;
			}
		}
		for (int j = 100; j < 200; j++){
			if (second.at(i) == data.at(j)){
				secondMatch++;
			}
		}
		for (int j = 200; j < 300; j++){
			if (second.at(i) == data.at(j)){
				thirdMatch++;
			}
		}
		for (int j = 300; j < 400; j++){
			if (second.at(i) == data.at(j)){
				fourthMatch++;
			}
		}
		for (int j = 400; j < 500; j++){
			if (second.at(i) == data.at(j)){
				fifthMatch++;
			}
		}
		for (int j = 0; j < 100; j++){
			if (second.at(i) == data.at(j)){
				sixthMatch++;
			}
		}
	}
	totalMatches[0] = firstMatch;
	totalMatches[1] = secondMatch;
	totalMatches[2] = thirdMatch;
	totalMatches[3] = fourthMatch;
	totalMatches[4] = fifthMatch;
	totalMatches[5] = sixthMatch;
	for (int i = 0; i < 6; i++){
		if (max <= totalMatches[i])
			max = totalMatches[i];
	}
	currentMatchNum = currentMatchNum + max;
	firstMatch = 0;
	secondMatch = 0;
	thirdMatch = 0;
	fourthMatch = 0;
	fifthMatch = 0;
	sixthMatch = 0;
	/////////////////////THIRD///////////////////////
	for (int i = 0; i < third.size(); i++){
		for (int j = 0; j < 100; j++){
			if (third.at(i) == data.at(j)){
				firstMatch++;
			}
		}
		for (int j = 100; j < 200; j++){
			if (third.at(i) == data.at(j)){
				secondMatch++;
			}
		}
		for (int j = 200; j < 300; j++){
			if (third.at(i) == data.at(j)){
				thirdMatch++;
			}
		}
		for (int j = 300; j < 400; j++){
			if (third.at(i) == data.at(j)){
				fourthMatch++;
			}
		}
		for (int j = 400; j < 500; j++){
			if (third.at(i) == data.at(j)){
				fifthMatch++;
			}
		}
		for (int j = 0; j < 100; j++){
			if (third.at(i) == data.at(j)){
				sixthMatch++;
			}
		}
	}
	totalMatches[0] = firstMatch;
	totalMatches[1] = secondMatch;
	totalMatches[2] = thirdMatch;
	totalMatches[3] = fourthMatch;
	totalMatches[4] = fifthMatch;
	totalMatches[5] = sixthMatch;
	for (int i = 0; i < 6; i++){
		if (max <= totalMatches[i])
			max = totalMatches[i];
	}
	currentMatchNum = currentMatchNum + max;
	firstMatch = 0;
	secondMatch = 0;
	thirdMatch = 0;
	fourthMatch = 0;
	fifthMatch = 0;
	sixthMatch = 0;
	//////////////////////FOURTH/////////////////////
	for (int i = 0; i < fourth.size(); i++){
		for (int j = 0; j < 100; j++){
			if (fourth.at(i) == data.at(j)){
				firstMatch++;
			}
		}
		for (int j = 100; j < 200; j++){
			if (fourth.at(i) == data.at(j)){
				secondMatch++;
			}
		}
		for (int j = 200; j < 300; j++){
			if (fourth.at(i) == data.at(j)){
				thirdMatch++;
			}
		}
		for (int j = 300; j < 400; j++){
			if (fourth.at(i) == data.at(j)){
				fourthMatch++;
			}
		}
		for (int j = 400; j < 500; j++){
			if (fourth.at(i) == data.at(j)){
				fifthMatch++;
			}
		}
		for (int j = 0; j < 100; j++){
			if (fourth.at(i) == data.at(j)){
				sixthMatch++;
			}
		}
	}
	totalMatches[0] = firstMatch;
	totalMatches[1] = secondMatch;
	totalMatches[2] = thirdMatch;
	totalMatches[3] = fourthMatch;
	totalMatches[4] = fifthMatch;
	totalMatches[5] = sixthMatch;
	for (int i = 0; i < 6; i++){
		if (max <= totalMatches[i])
			max = totalMatches[i];
	}
	currentMatchNum = currentMatchNum + max;
	firstMatch = 0;
	secondMatch = 0;
	thirdMatch = 0;
	fourthMatch = 0;
	fifthMatch = 0;
	sixthMatch = 0;
	////////////////////////FIFTH////////////////////////////////
	for (int i = 0; i < fifth.size(); i++){
		for (int j = 0; j < 100; j++){
			if (fifth.at(i) == data.at(j)){
				firstMatch++;
			}
		}
		for (int j = 100; j < 200; j++){
			if (fifth.at(i) == data.at(j)){
				secondMatch++;
			}
		}
		for (int j = 200; j < 300; j++){
			if (fifth.at(i) == data.at(j)){
				thirdMatch++;
			}
		}
		for (int j = 300; j < 400; j++){
			if (fifth.at(i) == data.at(j)){
				fourthMatch++;
			}
		}
		for (int j = 400; j < 500; j++){
			if (fifth.at(i) == data.at(j)){
				fifthMatch++;
			}
		}
		for (int j = 0; j < 100; j++){
			if (fifth.at(i) == data.at(j)){
				sixthMatch++;
			}
		}
	}
	/////////////////SIXTH/////////////////////////
	totalMatches[0] = firstMatch;
	totalMatches[1] = secondMatch;
	totalMatches[2] = thirdMatch;
	totalMatches[3] = fourthMatch;
	totalMatches[4] = fifthMatch;
	totalMatches[5] = sixthMatch;
	for (int i = 0; i < 6; i++){
		if (max <= totalMatches[i])
			max = totalMatches[i];
	}
	currentMatchNum = currentMatchNum + max;
	firstMatch = 0;
	secondMatch = 0;
	thirdMatch = 0;
	fourthMatch = 0;
	fifthMatch = 0;
	sixthMatch = 0;
	for (int i = 0; i < sixth.size(); i++){
		for (int j = 0; j < 100; j++){
			if (sixth.at(i) == data.at(j)){
				firstMatch++;
			}
		}
		for (int j = 100; j < 200; j++){
			if (sixth.at(i) == data.at(j)){
				secondMatch++;
			}
		}
		for (int j = 200; j < 300; j++){
			if (sixth.at(i) == data.at(j)){
				thirdMatch++;
			}
		}
		for (int j = 300; j < 400; j++){
			if (sixth.at(i) == data.at(j)){
				fourthMatch++;
			}
		}
		for (int j = 400; j < 500; j++){
			if (sixth.at(i) == data.at(j)){
				fifthMatch++;
			}
		}
		for (int j = 0; j < 100; j++){
			if (sixth.at(i) == data.at(j)){
				sixthMatch++;
			}
		}
	}
	totalMatches[0] = firstMatch;
	totalMatches[1] = secondMatch;
	totalMatches[2] = thirdMatch;
	totalMatches[3] = fourthMatch;
	totalMatches[4] = fifthMatch;
	totalMatches[5] = sixthMatch;
	for (int i = 0; i < 6; i++){
		if (max <= totalMatches[i]){
			max = totalMatches[i];
		}
	}
	currentMatchNum = currentMatchNum + max;
	
	double accuracy = currentMatchNum * 100.0 / 600.0;
	currentMatchNum = 0;
	
	
	return accuracy;
	//current iteration accuracy
}

void prepare(string filename, int h, dataVector& centroids, dataVector& data){
	//prepare vector for kmeans
	ifstream file;
	file.open(filename);
	string line;
	labelVector labels;
	srand(time(NULL));
	int random[6] = { rand() % 600, rand() % 600, rand() % 600, rand() % 600, rand() % 600, rand() % 600 };
	int lineCount = 0;
	while (getline(file, line)){
		lineCount++;
		istringstream iss(line);
		labels.clear();
		split(line, ' ', labels);
		labels.erase(labels.begin()+h,labels.begin()+60);
		data.push_back(labels);

		if (lineCount == random[1] || lineCount == random[2] || lineCount == random[3] || lineCount == random[4] || lineCount == random[5] || lineCount == random[0]) {
			if (centroids.size() != 6){
				centroids.push_back(labels);
			}
		}

	}
}

void prepareDCT(string filename, int h, dataVector& centroids, dataVector& data){
	
	ifstream file;
	file.open(filename);
	string line;
	labelVector labels;
	labelVector newLabels;
	srand(time(NULL));
	int random[6] = { rand() % 600, rand() % 600, rand() % 600, rand() % 600, rand() % 600, rand() % 600 };
	int lineCount = 0;
	double dataF = 0.0;
	while (getline(file, line)){
		lineCount++;
		istringstream iss(line);
		labels.clear();
		split(line, ' ', labels);
		
		for (int j = 0; j < 1; j++){
			for (int k = 0; k < 60; k++){
				dataF = dataF + labels.at(k)*cos(((2 * k + 1)*j*atan(1) * 4) / (2 * 60));
			}
			
			dataF = dataF*sqrt(1.0 / 60.0);
			
			newLabels.push_back(dataF);
			
		}
		dataF = 0.0;
		for (int j = 1; j < 60; j++){
			for (int k = 0; k < 60; k++){
				dataF = dataF + labels.at(k)*cos(((2 * k + 1)*j*atan(1) * 4) / (2 * 60));
			}
			
			dataF = dataF*sqrt(2.0 / 60.0);
			newLabels.push_back(dataF);
			dataF = 0.0;


	}

			
		data.push_back(newLabels);
		newLabels.clear();
		
	

		if (lineCount == random[1] || lineCount == random[2] || lineCount == random[3] || lineCount == random[4] || lineCount == random[5] || lineCount == random[0]) {
			if (centroids.size() != 6){
				centroids.push_back(labels);
			}
		}

	}
	
	for (int i = 0; i < data.size(); i++){
		data.at(i).erase(data.at(i).begin() + h, data.at(i).begin() + 60);
	}
}
float kmeans(string filename, dataVector& centroids, dataVector& data,int h){
	cout << "Calculating accuracy for k-means..." << endl;
	int iterationCount = 0;
	double accuracy = 0.0;
	int counter = 0;
	int clusterSizes[6] = { 0, 0, 0, 0, 0, 0 };
	bool centroidsFound = false;
	while (counter < 10){
		counter++;
		accuracy = centroidFinder(centroids, data, h, centroidsFound, clusterSizes); //do iterations here
		
	}	
	
	cout << "Accuracy for k-means is : %" << accuracy << endl;
	return accuracy;
}




float dct(string filename, int h){
	dataVector data;
	dataVector centroids;
	float dctAcc = 100.0;
	prepareDCT(filename, h, centroids, data);
	centroids.clear();
	int random[6] = { rand() % 600, rand() % 600, rand() % 600, rand() % 600, rand() % 600, rand() % 600 };
	centroids.push_back(data.at(random[0])); //RANDOM CENTROIDS
	centroids.push_back(data.at(random[1]));
	centroids.push_back(data.at(random[2]));
	centroids.push_back(data.at(random[3]));
	centroids.push_back(data.at(random[4]));
	centroids.push_back(data.at(random[5]));

	cout << "After DCT Compression  with h="<< h<< " ..." << endl;
	dctAcc = kmeans(filename, centroids, data, h); //do kmeans with given h
	return dctAcc; // return accuracy
	
}

int main(){
	string fileName = "synthetic_control.data";
	fileName = fileName.c_str();
	dataVector centroids;
	dataVector data;
	float accuracy = 0.0;
	float dctAcc = 100.0;
	prepare(fileName, 60, centroids, data); // reads from file, prepares the data
	accuracy = kmeans(fileName, centroids, data, 60); //finding kmeans accuracy before DCT
	centroids.clear(); // centroids are re-created in dct function
	data.clear(); // data is re-created in dct function
	int h = 59;
	while (dctAcc > accuracy*0.9){
		dctAcc = dct(fileName, h); // Returns accuracy with the current h
		h--;
	}

	cout << "Cut-off h: " << h+1 << endl;//FINAL H VALUE
	
	
	


	system("PAUSE");
	return 0;
}
