import turtle

# beállítja az lpszíneket és a kezdőpozíciót ----------
screen = turtle.Screen()
screen.bgcolor('white')
trtl = turtle.Turtle(shape="turtle")
trtl.pen(pencolor='orange', pensize=5)
trtl.up()
trtl.setpos(40 - screen.window_width()/2, screen.window_height()/2 - 25)
trtl.down()
# ------------------------------------------------------

# C
trtl.circle(-30, -180)

# pos
trtl.up()
trtl.right(135)
trtl.forward(85)
trtl.right(50)
trtl.down()

# S
trtl.circle(-15, -180)
trtl.circle(15, -180)

# pos
trtl.up()
trtl.left(50)
trtl.forward(85)
trtl.right(45)
trtl.down()

# O
trtl.circle(-30, -360)

#pos
trtl.up()
trtl.forward(50)
trtl.left(90)
trtl.forward(-60)
trtl.down()

# N
trtl.forward(60)
trtl.right(150)
trtl.forward(70)
trtl.left(150)
trtl.forward(60)

#pos
trtl.up()
trtl.right(90)
trtl.forward(50)
trtl.down()

#G
trtl.circle(-30, -270)
trtl.right(90)
trtl.forward(25)

#pos
trtl.up()
trtl.right(90)
trtl.forward(30)
trtl.right(90)
trtl.forward(70)
trtl.down()

#O
trtl.circle(-30, -360)

#pos
trtl.up()
trtl.circle(-30, -180)
trtl.right(180)
trtl.forward(50)
trtl.left(90)
trtl.down()

#R
trtl.forward(60)
trtl.right(90)
trtl.circle(-15, 180)
trtl.left(130)
trtl.forward(40)

turtle.done()