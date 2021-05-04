# BehaviourTreeSPE
## This library is designed to be used along with Unity

There is 5 different type of nodes :
`Nodes` - that are the basic one (Condition/Action)
`Sequence` - Execute all children node until a FAILURE encountered or the end (AND Gate)
`Selector` - Execute all children node until a SUCCESS encountered or the end (OR Gate)

In the follow example, you need to create your Tree by starting by the most down/left

                                                                                                                                                                                                    
                                                                                         `y////////////////////////////////////y.                                                                       
                                                                                         `y                                    o-                                                                       
                                                                                         `y               START                o-                                                                       
                                                                                         `y               START                o-                                                                       
                                                                                         `y               START                o-                                                                       
                                                                                         `y                                    o-                                                                       
                                                                                         `y````````````````````````````````````s-                                                                       
                                                                                          /::::::::::::::::::d:::::::::::::::::/`                                                                       
                                                                                                             h                                                                                          
                                                                                                             h                                                                                          
                                                                                                             y`                                                                                         
                                                                                          :::::::::::::::::::h::::::::::::::::::`                                                                       
                                                                                          h``````````````````.`````````````````s-                                                                       
                                                                                          h              SELECTOR              o-                                                                       
                                                                                          h              SELECTOR              o-                                                                       
                                                                                          h              SELECTOR              o-                                                                       
                                                                                          h              SELECTOR              o-                                                                       
                                                                                          h                                    o-                                                                       
                                                                                          h....................................s-                                                                       
                                                                                          --------:/os/--------:+so+++/---------`                                                                       
                                                                                               .://-.              `..-://:--.`                                                                         
                                                                                           .-//-.                         `..-://:--.`                                                                  
                                                                                       `-:/:.`                                   `..-://::--.`                                                          
                                                                                   `.://-`                                              `..-:///:--.`                                                   
                                                                                .-//-.                                                          `.--://:--.`                                            
                                                                            `-//:.`                                                                .---://+oso/:----------------------------`           
                                                                        `.:+:-`                                                                    /+...........-..........................o:           
                                                :---------------------:oso/---------                                                               //               ROTATING               +:           
                                                h.................................-y                                                               //               ROTATING               +:           
                                                y                                 `y                                                               //               ROTATING               +:           
                                                y            SEQUENCE             `y                                                               //               ROTATING               +:           
                                                y            SEQUENCE             `y                                                               //                                      +:           
                                                y            SEQUENCE             `y                                                               //                                      o:           
                                                y                                 `y                                                               ./::::::::::::::::::::::::::::::::::::::/.           
                                                h                                 `y                                                                                                                    
                                                o//////////++oso////y/////////oso+/+                                                                                                                    
                                                     ``-://::-`    .y                                                                                     
                                               ``.://::-`          s-                                                                                     
                                         ``-://::-`               .y                                                                                      
                                   ``.://::-`                     s-                                                                                      
                                 -/++//-`                        .y                                                                             
           y:::::::::::::::::/+o/:::s-          h:::::::::::::::::::::::::::s-                                                                            
           h                        +:          y                           o-                                                                            
           h    IS PLAYER IN SIGHT  +:          y    CHANNELING/SHOOTING    o-                                                                            
           h    IS PLAYER IN SIGHT -h:          y    CHANNELING/SHOOTING    o-                                                                            
           h    IS PLAYER IN SIGHT .o:          y    CHANNELING/SHOOTING    o-                                                                            
           h                        +:          y                           o-                                                                            
           h////////////////////////y:          h...........................s-                                                                            
                                                                                                                      
                                                                                                                            