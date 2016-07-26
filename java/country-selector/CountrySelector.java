import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.ComponentOrientation;
import java.awt.Container;
import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.Scanner;

import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JFrame;
import javax.swing.JList;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JLabel;
import javax.swing.JTextArea;
import javax.swing.border.EtchedBorder;
import javax.swing.border.TitledBorder;


public class CountrySelector extends JFrame {
	private static final int FRAME_WIDTH = 400;
	private static final int FRAME_HEIGHT = 300;



	private JTextArea text; // textArea defined
	private JComboBox CountryList; //ComboBox defined
	private JPanel CountryPanel; //jPanel defined

	public CountrySelector(){
		setSize(FRAME_WIDTH, FRAME_HEIGHT); // setting size
		CountryPanel = new JPanel(); 
		text = new JTextArea(20,30);
		String cList[] = {"Afghanistan", "Cuba", "Japan","Iraq", "Italy", "North Korea", "Peru", "Russia", "Serbia", "Turkey"}; //country list to be added to combobox
		CountryList = new JComboBox(cList);
		ActionListener listen = new CountryListener(); //adding the listener
		CountryList.addActionListener(listen);

		CountryPanel.add(CountryList); //adding ComboBox to panel
		CountryList.setBackground(Color.gray); //color change 
		CountryPanel.setBackground(Color.gray);

		CountryPanel.add(text);
		add(CountryPanel, BorderLayout.NORTH);
	}

	public class CountryListener implements ActionListener{

		@Override
		public void actionPerformed(ActionEvent arg0) {
			try {
				text.setText(null); // set text to null each time for a better look
				String coSelect = (String) CountryList.getSelectedItem();
				if (coSelect.equals("Afghanistan")){ // connection and getting data for afghanistan
					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/af.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine; 



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-26, inputLine.length()-19);
							text.append("Area of Afghanistan: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 47);
							text.append("Population of Afghanistan: " + inputLine + "\n");
						}


					}
					in.close();
				}

				else if (coSelect.equals("Cuba")){ //// connection and getting data for Cuba

					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/cu.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-26, inputLine.length()-19);
							text.append("Area of Cuba: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 47);
							text.append("Population of Cuba: " + inputLine + "\n");
						}


					}
					in.close();
				}
				if (coSelect.equals("Japan")){ // connection and getting data for Japan
					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/ja.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-26, inputLine.length()-19);
							text.append("Area of Japan: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 48);
							text.append("Population of Japan: " + inputLine + "\n");
						}



					}
					in.close();
				}
				if (coSelect.equals("Iraq")){ //// connection and getting data for iraq

					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/iz.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-26, inputLine.length()-19);
							text.append("Area of Iraq: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 47);
							text.append("Population of Iraq: " + inputLine + "\n");
						}



					}
					in.close();
				}
				if (coSelect.equals("Italy")){ //// connection and getting data for italy

					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/it.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-26, inputLine.length()-19);
							text.append("Area of Italy: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 47);
							text.append("Population of Italy: " + inputLine + "\n");
						}



					}
					in.close();
				}
				if (coSelect.equals("North Korea")){ // // connection and getting data for north korea

					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/kn.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-26, inputLine.length()-19);
							text.append("Area of North Korea: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 47);
							text.append("Population of North Korea: " + inputLine + "\n");
						}



					}
					in.close();
				}
				if (coSelect.equals("Peru")){ //// connection and getting data for peru

					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/pe.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){ 
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-26, inputLine.length()-19);
							text.append("Area of Peru: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 47);
							text.append("Population of Peru: " + inputLine + "\n");
						}


					}
					in.close();
				}
				if (coSelect.equals("Russia")){ // connection and getting data for russia

					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/rs.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-28, inputLine.length()-19);
							text.append("Area of Russia: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 48);
							text.append("Population of Russia: " + inputLine + "\n");
						}



					}
					in.close();
				}
				if (coSelect.equals("Serbia")){ // connection and getting data for serbia

					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/ri.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-25, inputLine.length()-19);
							text.append("Area of Serbia: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 47);
							text.append("Population of Serbia: " + inputLine + "\n");
						}



					}
					in.close();
				}
				if (coSelect.equals("Turkey")){ // connection and getting data for turkey

					URL myURL = new URL("https://www.cia.gov/library/publications/the-world-factbook/geos/tu.html");
					URLConnection myURLConnection = myURL.openConnection();



					Scanner in = new Scanner(new InputStreamReader(myURLConnection.getInputStream()));
					String inputLine;



					while (in.hasNextLine()){
						inputLine = in.nextLine();
						if(inputLine.contains("sq km<")){
							inputLine = inputLine.substring(inputLine.length()-26, inputLine.length()-19);
							text.append("Area of Turkey: " + inputLine + "\n");
						}

						if(inputLine.contains("(July")){
							inputLine = inputLine.substring(37, 47);
							text.append("Population of Turkey: " + inputLine + "\n");
						}


					}
					in.close();
				}}
			catch (MalformedURLException e) {  //catching exceptions
				System.out.println("Failed");
			} 
			catch (IOException e) {   
				System.out.println("Failed");
			}
			catch (NullPointerException e){
				System.out.println(e.getCause());
			}

		}
	}
}