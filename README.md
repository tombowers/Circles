Circles
=======

Demo app for Android and iOS using the Xamarin framework.

The app itself is a single view app containing a circle. The colour of the circle is retrieved from the ColourLovers API. Tapping the circle retrieves a new colour. Double tapping the circle toggles the display of the name of the colour. Tapping elsewhere in the view creates a new circle. Colour changes, and the adding of new circles is animated.

The app demonstrates good practices for creating draggable views with support for tap and double tap events. It also demonstrates sharing of code between platforms using a portable class library project. This core project contains the code for asynchronously connecting to the ColourLovers API, and shared interfaces for providing a unified touch event API.
